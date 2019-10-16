using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace Receive
{
    class Program
    {
        static private bool flag = false;
        static void startReceiver(ConnectionFactory factory,IConnection connection)
        {
            var channel = connection.CreateModel();
            while(!flag)
            {
                channel.QueueDeclare(queue: "SendToReceive",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [Server] Received {0}", message);
                    
                };
                channel.BasicConsume(queue: "SendToReceive",
                                    autoAck: true,
                                    consumer: consumer);
            }
            channel.Close();
        }

        static void startSending(ConnectionFactory factory,IConnection connection)
        {
            string message="";
            var channel = connection.CreateModel();
            while(!(message.Equals("0")))
            {

                message ="";
                channel.QueueDeclare(queue: "ReceiveToSend",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
                
                
                message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);

                
                channel.BasicPublish(exchange: "",
                                    routingKey: "ReceiveToSend",
                                    basicProperties: null,
                                    body: body);
                Console.WriteLine(" [Server] Sent {0}", message);
            }
            channel.Close();
            flag = true;
            Console.WriteLine("Communication end");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Server started...");
            var factory = new ConnectionFactory() { HostName = "localhost", RequestedHeartbeat=30 };
            var connection = factory.CreateConnection();
            Task first = Task.Run(() => startReceiver(factory,connection));

            Task second = Task.Run(() => startSending(factory,connection));
            Task[] tasks = {first,second};
            Task.WaitAll(tasks);
            connection.Close();
            Console.WriteLine("Main thread done");
        }   
    }
}

