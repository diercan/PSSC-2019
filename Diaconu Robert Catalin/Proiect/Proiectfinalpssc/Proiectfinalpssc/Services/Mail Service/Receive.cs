using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Proiectfinalpssc.Services.Mail_Service
{
    public class Receive
    {
        public Receive(string message)
        {//send de fapt
            var factory = new ConnectionFactory() {
                // HostName = "localhost"
                Uri = new Uri("amqp://znjrxrfe:iH-degHzbWE-TDX7QH9SsG4pxLO_CglJ@hawk.rmq.cloudamqp.com/znjrxrfe") 
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