using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Coada
{
    public class Spalatorie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int Number { get; set; }
        public int Price { get; set; }
        public string PhotoPath { get; set; }
    }
}
