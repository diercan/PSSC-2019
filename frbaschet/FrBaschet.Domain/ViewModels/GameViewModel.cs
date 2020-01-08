using System;
using System.ComponentModel.DataAnnotations;

namespace FrBaschet.Domain.ViewModels
{
    public class GameViewModel
    {
        public DateTime Date { get; set; }
        [Display(Name = "HomeTeam")]
        public TeamViewModel HomeTeam { get; set; }
        [Display(Name = "AwayTeam")]
        public TeamViewModel AwayTeam { get; set; }
        
    }
}