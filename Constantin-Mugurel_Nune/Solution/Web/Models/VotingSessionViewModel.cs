using Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class VotingSessionViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nume")]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        [Display(Name = "Data scadenta")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Candidati")]
        public ICollection<CandidateViewModel> Candidates { get; set; }
    }
}