using Newtonsoft.Json;
using Proiect_PSSC.Models;
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
            _factory = new ConnectionFactory { Uri = new Uri("amqp://papxernn:xuzUnWIeXaCQSPx79hwVa5fSKIqTgBTK@stingray.rmq.cloudamqp.com/papxernn") };
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
                    var pacient = JsonConvert.DeserializeObject<Pacient>(message);
                    var coeficientToSend = CalculateCoeficient(pacient);
                    await SendMessage(coeficientToSend, queueSend);
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

        public string CalculateCoeficient(Pacient pacient)
        {
            float inaltime = 0;
            float greutate = 0;

            try
            {
                inaltime = float.Parse(pacient.Inaltime.Split(" ")[0]);
                greutate = float.Parse(pacient.Greutate.Split(" ")[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.ToString());
            }
            var coeficient = Convert.ToString(greutate / (inaltime * inaltime));
            return coeficient;
        }
    }   
}
