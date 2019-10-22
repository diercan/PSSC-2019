using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Program
    {
        public static void Main()
        {
        var factory = new ConnectionFactory() {  Uri =new Uri( "amqp://niifhhdp:gdvQnylJbzwz7zRYe2YccrCS3cRNNXQ2@dove.rmq.cloudamqp.com/niifhhdp" )};
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
        }
    }
}
