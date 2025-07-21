using Confluent.Kafka;

namespace worker
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092", 
                GroupId = "loja-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("pedidos"); 

            Console.WriteLine("Consumidor Kafka iniciado. Aguardando mensagens...");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = consumer.Consume(stoppingToken);
                        Console.WriteLine($"Mensagem recebida do Kafka: {result.Message.Value}");
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"Erro ao consumir mensagem: {ex.Message}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
               
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
