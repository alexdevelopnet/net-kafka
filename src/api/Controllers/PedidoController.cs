using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly LojaContext _context;
        private readonly KafkaProducerService _kafkaProducer;
        public PedidoController(LojaContext context, KafkaProducerService kafkaProducer)
        {
            _context = context;
            _kafkaProducer = kafkaProducer;
        }
 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.PedidoProdutos)
                .ThenInclude(pp => pp.Produto)
                .ToListAsync();
        }
 
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.PedidoProdutos)
                .ThenInclude(pp => pp.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pedido == null) return NotFound();
            return pedido;
        }
 
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(PedidoDto pedidoDto)
        {
            var pedido = new Pedido
            {
                Data = DateTime.UtcNow,
                Status = "Pendente",
                PedidoProdutos = pedidoDto.Produtos.Select(pp => new PedidoProduto
                {
                    ProdutoId = pp.ProdutoId,
                    Quantidade = pp.Quantidade
                }).ToList()
            };
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
 
            await _kafkaProducer.PublishAsync(new
            {
                PedidoId = pedido.Id,
                Data = pedido.Data,
                Status = pedido.Status,
                Produtos = pedido.PedidoProdutos.Select(pp => new { pp.ProdutoId, pp.Quantidade })
            });
 
            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, new {
                pedido.Id,
                pedido.Data,
                pedido.Status,
                Produtos = pedido.PedidoProdutos.Select(pp => new { pp.ProdutoId, pp.Quantidade })
            });
        }
 
        [HttpPut("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] string status)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();
            pedido.Status = status;
            await _context.SaveChangesAsync();
            return NoContent();
        }
 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    // DTO para criar pedido
    public class PedidoDto
    {
        public List<PedidoProdutoDto> Produtos { get; set; } = new();
    }
    public class PedidoProdutoDto
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
} 