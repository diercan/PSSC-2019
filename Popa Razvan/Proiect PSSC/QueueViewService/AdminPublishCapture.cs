using System;
using System.Threading.Tasks;
using MassTransit;
using NotificationClass;

namespace QueueViewService
{
    class AdminPublishCapture : IConsumer<ReservationDeleted>
    {
        public async Task Consume(ConsumeContext<ReservationDeleted> context)
        {
            var message = $"Reservation deleted by admin: {context.Message.FirstName} {context.Message.LastName} {context.Message.Date.ToLongDateString()}";
            await Console.Out.WriteLineAsync(message);
        }
    }
}
