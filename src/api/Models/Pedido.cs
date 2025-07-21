using System;
using System.Collections.Generic;

namespace api.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public List<PedidoProduto> PedidoProdutos { get; set; } = new();
        public string Status { get; set; } = "Pendente";
    }

    public class PedidoProduto
    {
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;
        public int Quantidade { get; set; }
    }
} 