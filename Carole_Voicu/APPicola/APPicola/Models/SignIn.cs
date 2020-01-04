using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APPicola.Models
{
    public class SignIn
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}