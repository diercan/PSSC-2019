using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace QueuePublish
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");


            while (true)
            {
                var msg = Console.ReadLine();
                Send(msg);
            }
            
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

        public static void Send(string mailTo)
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

          
                var body = Encoding.UTF8.GetBytes(mailTo);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
               
            }

           

        }
    }
}
