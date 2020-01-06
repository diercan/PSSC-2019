using System;
using System.Threading.Tasks;
using MassTransit;
using NotificationClass;

namespace QueueViewService
{
    class ClientPublishCapture : IConsumer<ReservationCreated>
    {
        public async Task Consume(ConsumeContext<ReservationCreated> context)
        {
            var message = $"Reservation created by client: {context.Message.FirstName} {context.Message.LastName} {context.Message.Date.ToLongDateString()}";
            await Console.Out.WriteLineAsync(message);
        }
    }
}
