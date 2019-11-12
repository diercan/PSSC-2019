using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace Worker
{
    class Program
    {
       public static void Main()
        {
        var factory = new ConnectionFactory() { Uri =new Uri( "amqp://niifhhdp:gdvQnylJbzwz7zRYe2YccrCS3cRNNXQ2@dove.rmq.cloudamqp.com/niifhhdp") };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "task_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);

                int dots = message.Split('.').Length - 1;
                Thread.Sleep(dots * 1000);

                Console.WriteLine(" [x] Done");
            };
            channel.BasicConsume(queue: "task_queue", autoAck: true, consumer: consumer);
            Console.ReadLine();
        }
        }
    }
}
