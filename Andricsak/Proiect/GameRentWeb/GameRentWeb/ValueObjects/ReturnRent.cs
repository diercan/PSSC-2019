using GameRentWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.ValueObjects
{
    public class ReturnRent
    {
        public RentOrder Rent { get; set; }
        public float UserBalance { get; set; }
    }
}
