using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRentWeb.Models
{
    public class MessageBroker
    {
        private readonly IConnectionFactory _factory;
        public MessageBroker()
        {
            _factory = new ConnectionFactory() { Uri = new Uri("amqp://zswjrhxx:USPn7uoCvEEPxLVGO0XrzjhK9wDx3Gwq@reindeer.rmq.cloudamqp.com/zswjrhxx") };
        }

        public async Task SendMessage(string message)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "rents",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                             routingKey: "rents",
                                             basicProperties: null,
                                             body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        public async Task<RentOrder> ReceiveMessage()
        {
            RentOrder rent;
            using (var connection = _factory.CreateConnection())
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
                    rent = JsonConvert.DeserializeObject<RentOrder>(message);             
                };
                channel.BasicConsume(queue: "rents",
                                     autoAck: true,
                                     consumer: consumer);
                return rent;
            }
        }
    }
}
