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
            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add("bootstrap.servers", "localhost:9092");
            Producer producer = new Producer(properties);

            Console.WriteLine("Enter message to publish -");
            string userInput = Console.ReadLine();
            await producer.Publish("user-input", userInput);
            Console.WriteLine("Published...");
        }
    }
}
