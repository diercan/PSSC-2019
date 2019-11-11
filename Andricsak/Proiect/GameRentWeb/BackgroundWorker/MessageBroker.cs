using GameRentWeb.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    class MessageBroker : IDisposable
    {
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        public MessageBroker()
        {
            _factory = new ConnectionFactory { Uri = new Uri("amqp://zswjrhxx:USPn7uoCvEEPxLVGO0XrzjhK9wDx3Gwq@reindeer.rmq.cloudamqp.com/zswjrhxx") };
            _connection = _factory.CreateConnection();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public async Task Receive(string queueReceive, string queueSend)
        {        
            var channel = _connection.CreateModel();
            
            channel.QueueDeclare(queue: queueReceive,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received from web app --- {0}\n", message);
                var rentOrder = JsonConvert.DeserializeObject<RentOrder>(message);
                RentOperations rentOper = new RentOperations(rentOrder);

                rentOrder = await rentOper.CalculatePayment(4f);

                var rentSent = JsonConvert.SerializeObject(rentOrder);

                await SendMessage(rentSent, queueSend);
            };
            channel.BasicConsume(queue: queueReceive,
                                    autoAck: true,
                                    consumer: consumer);
            Console.ReadLine();
        }

        public async Task SendMessage(string message,string messageQueue)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: messageQueue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                             routingKey: messageQueue,
                                             basicProperties: null,
                                             body: body);
                Console.WriteLine("Sent to web app --- {0}\n", message);
            }
}
    }
}
