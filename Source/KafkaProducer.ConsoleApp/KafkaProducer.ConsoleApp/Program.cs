using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KafkaProducer.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await PublishMessages();
        }

        private static async Task PublishMessages()
        {
            // create producer
            Dictionary<string, string> properties = new Dictionary<string, string>
            {
                { "bootstrap.servers", "localhost:9092" } // address to the broker or cluster
            };
            Producer producer = new Producer(properties);

            // publish message
            Console.WriteLine("Enter message to publish -");
            string userInput = Console.ReadLine();
            var deliveryResult = await producer.Publish("user_input", userInput);
            Console.WriteLine("Message published to topic " + deliveryResult.Topic + " Partition " + deliveryResult.Partition
                + " Offset " + deliveryResult.Offset);
        }
    }
}
