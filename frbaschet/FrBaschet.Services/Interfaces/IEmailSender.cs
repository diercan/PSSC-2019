using FrBaschet.Services.Models;

namespace FrBaschet.Services.Interfaces
{
    public interface IEmailSender
    {
        void SendEmailAsync(EmailModel emailModel);
    }
}