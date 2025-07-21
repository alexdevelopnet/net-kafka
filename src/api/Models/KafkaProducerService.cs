using Confluent.Kafka;
using System.Text.Json;

namespace api.Models
{
    public class KafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic = "pedidos";

        public KafkaProducerService(string bootstrapServers)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task PublishAsync(object message)
        {
            var json = JsonSerializer.Serialize(message);
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = json });
        }
    }
} 