using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumb { get; set; }



    }
}
