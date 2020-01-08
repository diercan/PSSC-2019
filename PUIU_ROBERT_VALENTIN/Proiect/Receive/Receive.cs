using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Receive
{
    public static void Main()
    {
        List<string> lines = new List<string>();
        string FileName = "Students_List.txt";
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        string projectName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        string[] partialpath = projectDirectory.Split(new string[] { projectName }, StringSplitOptions.None);
        string path = partialpath[0] + FileName;
        using (FileStream fs = File.Create(path))
        { }

            var factory = new ConnectionFactory() 
            {
                Uri = new Uri("amqp://zdolgzsq:NAIaHujVMF-WNTxXa7kZ4z5jU5R-BUdB@dove.rmq.cloudamqp.com/zdolgzsq"),
                UserName = "zdolgzsq" ,
                Password = "NAIaHujVMF-WNTxXa7kZ4z5jU5R-BUdB",
            };
        using (var connection = factory.CreateConnection())
        using (System.IO.StreamWriter file =
       new System.IO.StreamWriter(path))
        {
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
                    file.WriteLine(message);
                    Console.WriteLine(" [x] Received {0}", message);
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