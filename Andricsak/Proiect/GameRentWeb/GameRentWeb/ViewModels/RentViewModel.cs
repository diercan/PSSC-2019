using GameRentWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.ViewModels
{
    public class RentViewModel
    {
        public RentOrder Rent { get; set; }
        [Display(Name= "Rented game")]
        public string RentedGame { get; set; }
    }
}
