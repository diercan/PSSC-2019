using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendEmail
{
    public class PostareConsumer : IConsumer<PostareImportantaCreata>
    {
        public async Task Consume(ConsumeContext<PostareImportantaCreata> context)
        {
            var message = $"\nEmail nou creat: {context.Message.Titlu} \n Body: {context.Message.Body} ";
            await Console.Out.WriteLineAsync(message);
        }
    }
}
