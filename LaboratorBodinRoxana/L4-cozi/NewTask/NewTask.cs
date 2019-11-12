using System;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

namespace NewTask
{
    class Program
    {
      
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() {  Uri =new Uri( "amqp://niifhhdp:gdvQnylJbzwz7zRYe2YccrCS3cRNNXQ2@dove.rmq.cloudamqp.com/niifhhdp" )};
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
        {
            
            channel.QueueDeclare(queue: "task_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            // string[] args;
            var message = GetMessage(args);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                     routingKey: "task_queue",
                     basicProperties: properties,
                     body: body);
        }
        }
        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}

