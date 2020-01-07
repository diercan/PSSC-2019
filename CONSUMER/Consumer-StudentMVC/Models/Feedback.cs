
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMVC.Models
{
    public class Feedback

    {
        public Guid Id { get; set; }

        [Required]
        public ProfesorList Profesor { get; set; }

        [Required]
        public string GoodFeedback { get; set; }

        public string BadFeedback { get; set; }
       
    }
    public enum ProfesorList
    {
        Albert_Moza,
        Genoveva_Inswards,
        Robert_Langdon,
        Dan_Brown,
        Abraham_Helsing

    }
}
