using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace EmailSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Email System Service Started!");
            Recieve();
            
        }



        public static void Recieve()
        {
            
           var factory = new ConnectionFactory() { Uri = new Uri("amqp://ebuoledk:XKCv80SfOT3Liwb52lMunrquIBnfcfyW@dove.rmq.cloudamqp.com/ebuoledk") };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Sending mail to {message.Split('$')[1]} identified by {message.Split('$')[0]} ");
                    EmailSystemService.SendConfirmationEmail(message.Split('$')[0], message.Split('$')[1]);
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
