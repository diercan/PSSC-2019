using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using APPicola.Models;

namespace Receive
{
    class Program
    {
        public static void Main()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://eoqhublt:bDVoI6WQ-EEsxMPV__5isF4E3KEu9LDb@shark.rmq.cloudamqp.com/eoqhublt"),
                UserName = "eoqhublt",
                Password = "bDVoI6WQ-EEsxMPV__5isF4E3KEu9LDb",
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "test", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var jsonText = JsonConvert.DeserializeObject<Articole>(message);

                    Console.WriteLine("S-a postat articolul numarul: {0}, cu numele {1}", $"{jsonText.index}", $"{jsonText.numearticol}");
                };

                channel.BasicConsume(queue: "test", autoAck: true, consumer: consumer);

                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}