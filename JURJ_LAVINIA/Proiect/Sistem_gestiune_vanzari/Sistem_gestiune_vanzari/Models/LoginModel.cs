using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistem_gestiune_vanzari.Models
{
    public class LoginModel
    {
        public int UserID { get; set; }
        [DisplayName("Username")]
        [Required(ErrorMessage ="Campul trebuie completat")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campul trebuie completat")]
        public string Password { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}