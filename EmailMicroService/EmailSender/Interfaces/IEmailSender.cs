using System.Net;
using System.Threading.Tasks;
using EmailSender.Models;

namespace EmailSender.Interfaces
{
    public interface IEmailSender
    {
        Task<HttpStatusCode> SendEmailAsync(EmailModel emailModel);
    }
}