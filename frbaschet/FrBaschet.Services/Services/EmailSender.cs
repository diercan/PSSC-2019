using System;
using System.Text;
using FrBaschet.Services.Interfaces;
using FrBaschet.Services.Models;
using Newtonsoft.Json;

namespace FrBaschet.Services.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly Sender _sender;

        public EmailSender()
        {
            _sender = new Sender();
            _sender.Connect();
        }

        public void SendEmailAsync(EmailModel emailModel)
        {
            Console.WriteLine("send");

            var channel = _sender.channel;
            channel.QueueDeclare("emails",
                true,
                false,
                false,
                null);

            var message = JsonConvert.SerializeObject(emailModel);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("",
                "emails",
                false,
                null,
                body);
            Console.WriteLine("email trimis");
        }
    }
}