
using MassTransit;
using StudentMVC.Models;
using StudentMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMVC
{

    public class MessageConsumer : IConsumer<Feedback>
    {
        public async Task Consume(ConsumeContext<Feedback> context)
        {
            var feedbackRepo = FeedbackRepoFactory.GetFeedbackRepo();
            feedbackRepo.CreateFeedback(context.Message);

            //await Console.Out.WriteLineAsync("subscriber:" + context.Message.Id);
           //await Console.Out.WriteLineAsync("subscriber:" + context.Message.GoodFeedback);

            //await Console.Out.WriteLineAsync("subscriber:" + context.Message.BadFeedback);



        }
    }
}

