using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrBaschet.Domain.Models;

namespace FrBaschet.Domain.Entities
{
    public class InvitationEntityModel : EntityModel
    {
        [Required]
        [StringLength(450)]
        [Index(IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required] public string Role { get; set; }

        public string Code { get; set; }
    }
}