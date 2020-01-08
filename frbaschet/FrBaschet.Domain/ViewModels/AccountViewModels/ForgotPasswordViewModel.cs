using System.ComponentModel.DataAnnotations;

namespace FrBaschet.Domain.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}