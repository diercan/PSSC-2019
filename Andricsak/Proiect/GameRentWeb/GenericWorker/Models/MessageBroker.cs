using GameRentWeb.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenericWorker
{
    class MessageBroker : IMessageBroker
    {
        private readonly IConnection _connection;
        private readonly ILogger<MessageBroker> _logMessage;
        public MessageBroker(ILogger<MessageBroker> logMessage, IConnection connection)
        {
            
            _logMessage = logMessage;
            _connection = connection;
        }

        public async Task Receive(string queueReceive)
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
                _logMessage.LogInformation($"{DateTimeOffset.Now} - Received from web app --- { message}\n" );
                var rentOrder = JsonConvert.DeserializeObject<RentOrder>(message);
                RentOperations rentOper = new RentOperations(rentOrder);

                _logMessage.LogInformation($"Queue for receiving triggered is: {queueReceive}\n");
                if (queueReceive == "ReturnToWorker")
                {
                    rentOrder = await rentOper.CalculateReturn(3f);
                    var rentSent = JsonConvert.SerializeObject(rentOrder);
                    _logMessage.LogInformation("Received a return command\n");
                    await SendMessage(rentSent, "ReturnToWeb");
                }
                else
                {
                    rentOrder = await rentOper.CalculatePayment(3f);
                    var rentSent = JsonConvert.SerializeObject(rentOrder);
                    _logMessage.LogInformation("Received rent/extend command\n");
                    await SendMessage(rentSent, "WorkerToRent");
                }
            };
            channel.BasicConsume(queue: queueReceive,
                                    autoAck: true,
                                    consumer: consumer);
            Console.ReadLine();
        }

        public async Task SendMessage(string message,string queueSend)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueSend,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                             routingKey: queueSend,
                                             basicProperties: null,
                                             body: body);
                _logMessage.LogInformation($"{DateTimeOffset.Now} - Sent to web app --- {message}\n");
            }
}
    }
}
