using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CandidateViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Prenume")]
        public string FirstName { get; set; }
        [Display(Name = "Nume")]
        public string LastName { get; set; }
        [Display(Name = "Partid")]
        public string PartyName { get; set; }
    }
}