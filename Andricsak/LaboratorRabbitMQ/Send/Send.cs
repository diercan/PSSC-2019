using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace Send
{
    class Program
    {
        static private bool flag = false;
        static void startReceiver(ConnectionFactory factory,IConnection connection)
        {
            var channel = connection.CreateModel();
            Console.WriteLine("Receiver start...");
            while(!flag)
            {                    
                channel.QueueDeclare(queue: "ReceiveToSend",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [Client] Received {0}", message);
                    Console.WriteLine("Trece dupa");
                };
                channel.BasicConsume(queue: "ReceiveToSend",
                                    autoAck: true,
                                    consumer: consumer);
                
            }
            channel.Close();
        }
        
        static void startSending(ConnectionFactory factory,IConnection connection)
        {
            var channel = connection.CreateModel();
            string message="";
            while(!(message.Equals("0")))
            {   
                message ="";
                
                channel.QueueDeclare(queue: "SendToReceive",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
                
                
                message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                    routingKey: "SendToReceive",
                                    basicProperties: null,
                                    body: body);
                Console.WriteLine(" [Client] Sent {0}", message);
                
            }
            channel.Close();
            flag = true;
            Console.WriteLine("Communication end");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Client started...");
            var factory = new ConnectionFactory() { HostName = "localhost",RequestedHeartbeat=30 };
            var connection = factory.CreateConnection();
            // start Receiver
            Task first = Task.Run(() => startReceiver(factory,connection));

            Task second = Task.Run(() => startSending(factory,connection));
            Task[] tasks = {first,second};
            Task.WaitAll(tasks);
            Console.WriteLine("Main thread done");
        }
    }
}

