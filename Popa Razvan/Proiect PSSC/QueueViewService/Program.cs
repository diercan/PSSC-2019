using System;
using MassTransit;

namespace QueueViewService
{
    class Program
    {
        static void Main(string[] args)
        {
            //Client publish reception
            var busClient = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("amqp://xmgksbxb:eXA0zziYMVWoHo0vw7-WVIfgm2Ru838A@stingray.rmq.cloudamqp.com/xmgksbxb"), h =>
                {
                    h.Username("xmgksbxb");
                    h.Password("eXA0zziYMVWoHo0vw7-WVIfgm2Ru838A");
                });

                sbc.ReceiveEndpoint(e =>
                {
                    e.Consumer<ClientPublishCapture>();
                });
            });

            busClient.Start();
            //
            //Admin publish reception
            var busAdmin = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("amqp://xvtmmbgz:1C2wLj9NWM_LhGwm1jvx_lofx79WxStQ@stingray.rmq.cloudamqp.com/xvtmmbgz"), h =>
                {
                    h.Username("xvtmmbgz");
                    h.Password("1C2wLj9NWM_LhGwm1jvx_lofx79WxStQ");
                });

                sbc.ReceiveEndpoint(e =>
                {
                    e.Consumer<AdminPublishCapture>();
                });
            });

            busAdmin.Start();
            //
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
