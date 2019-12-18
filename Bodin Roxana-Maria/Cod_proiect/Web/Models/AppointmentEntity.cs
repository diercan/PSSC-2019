using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Web.Models
{
    public class AppointmentEntity : TableEntity
    {
        public int Id{ get; set;}
        public string MedName{ get; set; }
        
        public DateTime Date { get; set; }   
        
        public string PacientName { get; set; } 
        public string BreedPet { get; set; } 
        public string Symptoms { get; set; } 
      
        public AppointmentEntity() { 
        
        }
    }
}