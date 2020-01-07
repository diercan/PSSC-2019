using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.IO;
using Json.Net;
using Newtonsoft.Json;
using Web.Models.DDD;

namespace Web.Models
{
    public class AppointmentDto
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string PatientName { get; set; }
 
        [Required]
        [Display(Name = "Appointment Date")]                 
        [DataType(DataType.Date)]  
        public DateTime DOB { get; set; }  
        [Required]
        [Display(Name = "Med Name")]
        public string MedName { get; set; } 
        [Required]
        [Display(Name = "Breed Pet")]
        public string BreedPet { get; set; } 
        [Required]
        [Display(Name = "Symptoms")]
        public string Symptoms { get; set; } 
        public string Status { get; set; } 
        
    }
}

        


