using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkinShopCSGO.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string Name { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "CNP is invalid")]
        public string CNP { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}