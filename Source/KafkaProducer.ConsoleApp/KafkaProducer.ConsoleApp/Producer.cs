using Confluent.Kafka;
using KafkaProducer.ConsoleApp.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KafkaProducer.ConsoleApp
{
    public class Producer
    {

        private readonly ProducerConfig _producerConfig;
        public Producer(Dictionary<string, string> config)
        {
            _producerConfig = new ProducerConfig(config);
        }
        
        public async Task<DeliveryResult<Null, TValue>> Publish<TValue>(string topic, TValue message)
        {
            using var producer = new ProducerBuilder<Null, TValue>(_producerConfig)
               .SetValueSerializer(new JsonSerializer<TValue>())
               .Build();
            var topicName = string.IsNullOrEmpty(topic) ? message.GetType().Name : topic;
            var deliveryResult = await producer.ProduceAsync(topicName, new Message<Null, TValue>() { Value = message });
            producer.Flush();
            producer?.Dispose();
            return deliveryResult;
        }

        public async Task<DeliveryResult<TKey, TValue>> Publish<TKey, TValue>(string topic, TKey key, TValue message)
        {
            using var producer = new ProducerBuilder<TKey, TValue>(_producerConfig)
               .SetValueSerializer(new JsonSerializer<TValue>())
               .Build();
            var topicName = string.IsNullOrEmpty(topic) ? message.GetType().Name : topic;
            var deliveryResult = await producer.ProduceAsync(topicName, new Message<TKey, TValue>() { Key = key, Value = message });
            producer.Flush();
            producer?.Dispose();
            return deliveryResult;
        }
    }
}
