using MassTransit;
using System;

namespace MessageConsole
{
    class Program
    {
        static void Main(string[] args)
		{ 
			var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
			{
				sbc.Host(new Uri("amqp://hwtjwzhp:AgmGeD5PSuocVNMgot9OncI595wDL7Uv@dove.rmq.cloudamqp.com/hwtjwzhp"), h =>
				{
					h.Username("hwtjwzhp");
					h.Password("AgmGeD5PSuocVNMgot9OncI595wDL7Uv");
				});

				sbc.ReceiveEndpoint(e =>
				{
					e.Consumer<EventConsumer>();
				});
			});

		bus.Start();

			Console.ReadKey();

		}
}
}
