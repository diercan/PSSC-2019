using MassTransit;
using System;

namespace SendEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("amqp://vkkjzmlp:JgF_kO2IPRHpu4mdmLavflHwkvr9Lws-@dove.rmq.cloudamqp.com/vkkjzmlp"), h =>
                {
                    h.Username("vkkjzmlp");
                    h.Password("JgF_kO2IPRHpu4mdmLavflHwkvr9Lws-");
                });

                sbc.ReceiveEndpoint(e =>
                {
                    e.Consumer<PostareConsumer>();
                });
            });

            bus.Start();
        }
    }
}
