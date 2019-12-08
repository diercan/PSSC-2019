using GameRentWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.ViewModels
{
    public class RentViewModel
    {
        public RentOrder Rent { get; set; }
        public string RentedGame { get; set; }
    }
}
