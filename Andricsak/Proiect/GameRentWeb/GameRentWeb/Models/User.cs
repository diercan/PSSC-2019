using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.Models
{
    public class User
    {     
        public int Id { get; set; }
        [Display(Name = "User name")]
        [Required(ErrorMessage ="Username is required!")]
        [StringLength(100,ErrorMessage = "Must be between 5 and 100 characters",MinimumLength =5)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required!"),DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required!"),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public float Balance { get; set; }
        public virtual ICollection<RentOrder> RentOrders { get; set; }
    }
}
