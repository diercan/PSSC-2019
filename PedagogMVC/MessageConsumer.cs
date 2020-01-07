using MassTransit;
using PedagogMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PedagogMVC
{
    public class MessageConsumer : IConsumer<ReceiveFeedback>
    {
        public async Task Consume(ConsumeContext<ReceiveFeedback> context)
        {
            await Console.Out.WriteLineAsync("subscriber:" + context.Message.Id);
            await Console.Out.WriteLineAsync("subscriber:" + context.Message.GoodFeedback);

            await Console.Out.WriteLineAsync("subscriber:" + context.Message.BadFeedback);



        }
    }
}
