using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.IO;
using Json.Net;
using Newtonsoft.Json;

namespace Web.Models
{
    public class Med
    {
        
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
 
        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }
        
    }
}