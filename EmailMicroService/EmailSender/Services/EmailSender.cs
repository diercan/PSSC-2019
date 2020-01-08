using System;
using System.Net;
using System.Threading.Tasks;
using EmailSender.Config;
using EmailSender.Interfaces;
using EmailSender.Models;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EmailSender.Services
{
    public class EmailSender : IEmailSender
    {
        private SendgridConfig _config;

        public EmailSender()
        {
            _config = new SendgridConfig();
        }

        public async Task<HttpStatusCode> SendEmailAsync(EmailModel emailModel)
        {
            var msg = MailHelper.CreateSingleEmail(
                emailModel.From,
                emailModel.To,
                emailModel.Subject,
                "",
                emailModel.HtmlContent);

            var response = await _config.Client.SendEmailAsync(msg);
            Console.WriteLine($"email trimis catre {emailModel.To.Email} with status {response.StatusCode}");

            return response.StatusCode;
        }
    }
}