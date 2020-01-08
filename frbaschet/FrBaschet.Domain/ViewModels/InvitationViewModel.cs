using System.ComponentModel.DataAnnotations;

namespace FrBaschet.Domain.ViewModels
{
    public class InvitationViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required] public string Role { get; set; }
    }
}