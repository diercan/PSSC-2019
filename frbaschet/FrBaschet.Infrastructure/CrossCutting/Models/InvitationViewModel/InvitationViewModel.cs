using System.ComponentModel.DataAnnotations;

namespace FrBaschet.Infrastructure.CrossCutting.Models.InvitationViewModel
{
    public class InvitationViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }

        public string Message { get; set; }
        public URole URole { get; set; }
    }

    public enum URole
    {
        Admin,
        Stuff
    }
}