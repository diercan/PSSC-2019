using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consola
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    double euro = 0.22D;
                    Console.WriteLine(string.Format("€{0:N2} Euro", Int32.Parse(message)* 0.22));
                    
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);
               
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

        }
    }
}
