using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace MessageConsole
{
    class EventConsumer : IConsumer<EventNotification>
    {
        public async Task Consume(ConsumeContext<EventNotification> context)
        {
            String eventNotificationMessage = $"A New Event Has Been Created On: {context.Message.date.ToShortDateString()}. Description: {context.Message.text}";
            await Console.Out.WriteLineAsync(eventNotificationMessage);
        }
    }
}
