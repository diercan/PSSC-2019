using ContractsExample;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace ConsoleExample
{
	public class LicenseConsumer 
		: IConsumer<LicenseCreated>,
		  IConsumer<LicenseDeleted>
	{
		public async Task Consume(ConsumeContext<LicenseCreated> context)
		{
			var message = $"New license added: {context.Message.Name}, Category: {context.Message.Category} ({context.Message.Description.ToString()})";
			await Console.Out.WriteLineAsync(message);
		}

		public async Task Consume(ConsumeContext<LicenseDeleted> context)
		{
			var message = $"License deleted: {context.Message.Name}, Category: {context.Message.Category} ({context.Message.Description.ToString()})";
			await Console.Out.WriteLineAsync(message);
		}
	}
}
