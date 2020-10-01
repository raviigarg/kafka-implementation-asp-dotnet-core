using Confluent.Kafka;
using KafkaProducer.ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KafkaProducer.ConsoleApp
{
    public class Publisher
    {

        private ProducerConfig _producerConfig;
        public Publisher(IDictionary<string, string> config)
        {
            _producerConfig = new ProducerConfig(config);
        }

        public async Task<DeliveryResult<Null, TValue>> Publish<TValue>(TValue message, string topic)
        {
            using var producer = new ProducerBuilder<Null, TValue>(_producerConfig)
               .SetValueSerializer(new JsonSerializer<TValue>())
               .Build();
            var topicName = String.IsNullOrEmpty(topic) ? message.GetType().Name : topic;
            var deliveryResult = await producer.ProduceAsync(topicName, new Message<Null, TValue>() { Value = message });
            producer.Flush();
            producer?.Dispose();
            return deliveryResult;
        }

        public async Task<DeliveryResult<TKey, TValue>> Publish<TKey, TValue>(TKey key, TValue message, string topic)
        {
            using var producer = new ProducerBuilder<TKey, TValue>(_producerConfig)
               .SetValueSerializer(new JsonSerializer<TValue>())
               .Build();
            var topicName = String.IsNullOrEmpty(topic) ? message.GetType().Name : topic;
            var deliveryResult = await producer.ProduceAsync(topicName, new Message<TKey, TValue>() { Key = key, Value = message });
            producer.Flush();
            producer?.Dispose();
            return deliveryResult;
        }
    }
}
