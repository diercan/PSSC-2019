using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SecretQuestionViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Intrebare")]
        public string Question { get; set; }
        [Display(Name = "Raspuns")]
        public string Answer { get; set; }
    }
}