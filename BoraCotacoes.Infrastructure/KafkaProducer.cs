using System.Text.Json;
using Confluent.Kafka;

namespace BoraCotacoes.Infrastructure
{
    public class KafkaProducer : IProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(string bootstrapServers)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task ProduceAsync<T>(string topic, T message)
        {
            var json = JsonSerializer.Serialize(message);

            var kafkaMessage = new Message<Null, string>
            {
                Value = json
            };

            try
            {
                var result = await _producer.ProduceAsync(topic, kafkaMessage);
            }
            catch (ProduceException<Null, string> ex)
            {
                Console.WriteLine($"❌ Falha ao enviar mensagem: {ex.Error.Reason}");
            }
        }
    }

}
