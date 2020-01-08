using FrBaschet.Domain.Models;
using FrBaschet.Services.Config;
using FrBaschet.Services.Models;

namespace FrBaschet.Services.Templates
{
    public class AccountConfirmationEmail : EmailModel
    {
        public AccountConfirmationEmail(EmailAddress to, string callbackUrl) : base(new EmailAddress
            {
                Email = Global.SenderEmail,
                Name = Global.Name
            }, "Confirm your email",
            to,
            $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>."
        )
        {
        }
    }
}