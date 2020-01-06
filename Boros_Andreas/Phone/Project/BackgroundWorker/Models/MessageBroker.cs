using Newtonsoft.Json;
using Project.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorker.Models
{
    class MessageBroker
    {
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        public MessageBroker()
        {
            _factory = new ConnectionFactory { Uri = new Uri("amqp://qgamjvrp:b5uAFWenQCSyWnNZ6DsSeA7fOYd6zir4@hedgehog.rmq.cloudamqp.com/qgamjvrp") };
            _connection = _factory.CreateConnection();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public async Task Receive(string queueReceive,string queueSend)
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueReceive,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            while (true)
            {
                
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"{DateTimeOffset.Now} Received from web app --- {message}\n");
                    var phone = JsonConvert.DeserializeObject<Phone>(message);
                    var rateToSend = CalculateRate(phone);
                    await SendMessage(rateToSend, queueSend);
                };
                channel.BasicConsume(queue: queueReceive,
                                        autoAck: true,
                                        consumer: consumer);
                consumer.Shutdown += Consumer_Shutdown;
                Thread.Sleep(2);
            }
        }

        private void Consumer_Shutdown(object sender, ShutdownEventArgs e)
        {
            
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
                Console.WriteLine("Sent to web app --- {0}\n", message);
            }
        }

        public string CalculateRate(Phone phone)
        {
            float price = 0;

            try
            {
                price = float.Parse(phone.Price.Split(" ")[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.ToString());
            }
            var rate = Convert.ToString(price / 12) + " RON / month";
            return rate;
        }
    }   
}
