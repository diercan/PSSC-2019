using FrBaschet.Domain.Models;
using FrBaschet.Services.Config;
using FrBaschet.Services.Models;

namespace FrBaschet.Services.Templates
{
    public class InvitationModel : EmailModel
    {
        public InvitationModel(EmailAddress to, string callbackUrl) : base(new EmailAddress
            {
                Email = Global.SenderEmail,
                Name = Global.Name
            }, "FrBaschet invitation",
            to,
            $"Please register by <a href='{callbackUrl}'>clicking here</a>."
        )
        {
        }
    }
}