using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GameRentWeb.Models
{
    public class Game
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] CoverImage { get; set; }
        public string Platform { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; } // this will decrease when an order is made
    }
}
