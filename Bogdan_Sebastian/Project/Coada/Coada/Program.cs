using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Coada
{
 
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ApplicationContext();

            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://zkchlziu:G_s895kf9DF-Z9kWuFNkyOVHH09Jgu0I@barnacle.rmq.cloudamqp.com/zkchlziu");
            factory.UserName = "zkchlziu";
            factory.Password = "G_s895kf9DF-Z9kWuFNkyOVHH09Jgu0I";
            var _channel = factory.CreateConnection().CreateModel();
            _channel.QueueDeclare(queue: "createSpalatorie",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                string message = Encoding.UTF8.GetString(body);
                SpalatorieDto o = JsonConvert.DeserializeObject<SpalatorieDto>(message);
                Spalatorie newSpalatorie = new Spalatorie
                {
                    Name = o.Name,
                    Number = o.Number,
                    Price = o.Price,
                    PhotoPath = o.PhotoPath
                };
                Spalatorie[] last = context.Spalatorii.ToArrayAsync().Result;
                if(last.Length != 0)
                {
                    newSpalatorie.Id = last[last.Length - 1].Id + 1;
                }
                context.Spalatorii.Add(newSpalatorie);
                context.SaveChanges();
                Console.WriteLine(message);
            };
            _channel.BasicConsume(queue: "createSpalatorie",
                autoAck: true,
                consumer: consumer);
        }
    }
}
