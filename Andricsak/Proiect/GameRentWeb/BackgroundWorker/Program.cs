using GameRentWeb.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace BackgroundWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var factory = new ConnectionFactory() { Uri = new Uri("amqp://zswjrhxx:USPn7uoCvEEPxLVGO0XrzjhK9wDx3Gwq@reindeer.rmq.cloudamqp.com/zswjrhxx") };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "rents",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    var rentOrder = JsonConvert.DeserializeObject<RentOrder>(message);
                    RentOperations rentOper = new RentOperations(rentOrder);
                    rentOrder = await rentOper.CalculatePayment(3f);

                    Console.WriteLine($"{rentOrder.ExpiringDate} -- {rentOrder.TotalPayment}");
                };
                channel.BasicConsume(queue: "rents",
                                     autoAck: true,
                                     consumer: consumer);

                
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }


    }
}
