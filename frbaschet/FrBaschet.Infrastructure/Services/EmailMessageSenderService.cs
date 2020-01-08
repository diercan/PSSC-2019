// using System;
// using System.Net.Mail;
// using System.Text;
// using FrBaschet.Domain.Interfaces;
// using RabbitMQ.Client;
//
// namespace FrBaschet.Infrastructure.Services
// {
//     public class EmailMessageSenderService : IMessageSender
//     {
//         public void SendNotificationEmail(string toAddress, string messageBody)
//         {
//             ConnectionFactory factory = new ConnectionFactory();
//             factory.Uri = new Uri("amqp://wlhlevup:ocnV54nMZQLLm9QNct2d5oyWJ_Ip0lug@shrimp.rmq.cloudamqp.com/wlhlevup");
//             factory.UserName = "wlhlevup";
//             factory.Password = "ocnV54nMZQLLm9QNct2d5oyWJ_Ip0lug";
//             var _conn = factory.CreateConnection();
//             var channel = _conn.CreateModel();
//
//             var queueName = "testGabi";
//
//             channel.QueueDeclareNoWait(queueName, true, false, false, null);
//             string message = "Hello World!";
//             var body = Encoding.UTF8.GetBytes(message);
//
//             channel.BasicPublish(exchange: "",
//                 routingKey: "hello",
//                 basicProperties: null,
//                 body: body);
//             Console.WriteLine(" [x] Sent {0}", message);
//         }
//     }
// }

