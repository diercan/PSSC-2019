using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Services
{
    public class MessageBroker
    {
        private readonly IConnection _connection;
        public MessageBroker()
        {

        }
        public MessageBroker(IConnection connection)
        {
            _connection = connection;
        }

        public async Task SendMailAsync(string mailTo)
        {

            //var factory = new ConnectionFactory() { Uri = new Uri("amqp://ebuoledk:XKCv80SfOT3Liwb52lMunrquIBnfcfyW@dove.rmq.cloudamqp.com/ebuoledk") };
            //using (var connection = factory.CreateConnection())
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                var body = Encoding.UTF8.GetBytes(mailTo);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);

            }



        }
    }
}
