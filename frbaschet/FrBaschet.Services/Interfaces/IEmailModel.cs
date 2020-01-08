using FrBaschet.Domain.Models;

namespace FrBaschet.Services.Interfaces
{
    public interface IEmailModel
    {
        EmailAddress From { get; set; }
        string Subject { get; set; }
        EmailAddress To { get; set; }
        string HtmlContent { get; set; }
    }
}