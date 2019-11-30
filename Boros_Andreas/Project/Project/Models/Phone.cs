using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Dimension { get; set; }
        [Required]
        public string Type { get; set; }

        public string Email { get; set; }

        public string Imagine { get; set; }

        public string PhotoPath { get; set; }

    }
}
