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
    public class MessageBroker : IDisposable
    {
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        public MessageBroker()
        {
            _factory = new ConnectionFactory() { Uri = new Uri("amqp://zswjrhxx:USPn7uoCvEEPxLVGO0XrzjhK9wDx3Gwq@reindeer.rmq.cloudamqp.com/zswjrhxx") };
            _connection = _factory.CreateConnection();
        }

        public async Task SendMessage(string message)
        {          
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: "RentToWorker",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                             routingKey: "RentToWorker",
                                             basicProperties: null,
                                             body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        public async Task<RentOrder> ReceiveMessage()
        {
            RentOrder rent = null;
            bool received = false;
            var channel = _connection.CreateModel();
            while (!received)
            {
                channel.QueueDeclare(queue: "WorkerToRent",
                                      durable: false,
                                      exclusive: false,
                                      autoDelete: false,
                                      arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Received from worker {0}", message);
                    rent = JsonConvert.DeserializeObject<RentOrder>(message);
                    received = true;
                };
                channel.BasicConsume(queue: "WorkerToRent",
                                        autoAck: true,
                                        consumer: consumer);
            }
            return await Task.FromResult<RentOrder>(rent);
        }
      
        public void Dispose()
        {
            _connection.Close();
        }
    }
}
