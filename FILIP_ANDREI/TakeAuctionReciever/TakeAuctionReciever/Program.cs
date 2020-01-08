using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using TakeAuctionReciever;

namespace TakeAuctionReceiver
{
    class Program
    {
        public static void Main()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://urfkyisb:eaRpMrMI8FRa2G-MWzpOIdfEw6Y8-6ND@baboon.rmq.cloudamqp.com/urfkyisb"),
                UserName = "urfkyisb",
                Password = "eaRpMrMI8FRa2G-MWzpOIdfEw6Y8-6ND",
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
                    var jsonText = JsonConvert.DeserializeObject<Licitatii>(message);

                    Console.WriteLine("S-a postat produsul: {0}, cu pretul {1}, de catre {2}", $"{jsonText.NumeProdus}", $"{jsonText.Pret}", $"{jsonText.User}");
                };

                channel.BasicConsume(queue: "test", autoAck: true, consumer: consumer);

                Console.WriteLine("Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
