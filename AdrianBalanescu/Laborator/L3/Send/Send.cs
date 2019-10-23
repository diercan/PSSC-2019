using System;
using RabbitMQ.Client;
using System.Text;
namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
        	var factory = new ConnectionFactory() { Uri = new Uri("amqp://tnnebomx:VribMSLf_GSrcw9TztTSgD1CZ5Nyl2wm@hawk.rmq.cloudamqp.com/tnnebomx") };
		using (var connection = factory.CreateConnection())
		{
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "task_queue",
							durable: true,
							exclusive: false,
							autoDelete: false,
							arguments: null);
			var message = GetMessage(args);
			var body = Encoding.UTF8.GetBytes(message);
			var properties = channel.CreateBasicProperties();
			properties.Persistent = true;
			channel.BasicPublish(exchange: "",
						routingKey: "task_queue",
						basicProperties: properties,
						body: body);
			System.Console.WriteLine("[x] Sent {0}", message);
			}

		}
		System.Console.WriteLine("Ready");
		System.Console.ReadLine();
    	}

		private static string GetMessage(string[] args)
		{
    		return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
		}
	}
}