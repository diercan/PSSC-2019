using MassTransit;
using System;

namespace ConsoleExample
{
	class Program
	{
		static void Main(string[] args)
		{
			var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
			{
				sbc.Host(new Uri("amqp://msmcpkzk:nlP6hJVvCr3mgyr8EI-XU1or5QatPXGe@dove.rmq.cloudamqp.com/msmcpkzk"), h =>
				{
					h.Username("msmcpkzk");
					h.Password("nlP6hJVvCr3mgyr8EI-XU1or5QatPXGe");
				});

				sbc.ReceiveEndpoint(e =>
				{
					e.Consumer<LicenseConsumer>();
				});
			});

			bus.Start();

			Console.WriteLine("Press any key to exit");
			Console.ReadKey();

		}
	}
}
