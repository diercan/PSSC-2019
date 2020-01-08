using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EmailSender.Interfaces;
using EmailSender.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sender = new global::EmailSender.Services.EmailSender();
            var channel = new global::ConsoleApp1.Receiver().channel;
                channel.QueueDeclare(queue: "emails",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    EmailModel o = JsonSerializer.Deserialize<EmailModel>(message);
                    var r = sender.SendEmailAsync(o).Result;
                    Console.WriteLine(message);
                    Console.WriteLine(r);
                };
                channel.BasicConsume(queue: "emails",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            // }
        }
    }
}