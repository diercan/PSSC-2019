using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeShop.Services
{
    public class Send
    {
        public Send(string message) //Receive-ul e defaptu un Send pentru proiect
        {
            var factory = new ConnectionFactory()
            {
                // HostName = "localhost"
                Uri = new Uri("amqp://sjlxrrww:VBdMZA0ABgfZ9jNeXCiBPjdWpdUrFkUe@dove.rmq.cloudamqp.com/sjlxrrww")
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            //Console.WriteLine(" Press [enter] to exit.");
            //Console.ReadLine();
        }
    }
}
