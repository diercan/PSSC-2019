using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdminSideAPP.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Reservation Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Reservation Hour")]
        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [Display(Name = "Number of seats")]
        [Required]
        public int SeatNumber { get; set; }

        [Display(Name = "Additional Details")]
        public string Details { get; set; }
    }
}
