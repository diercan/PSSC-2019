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

namespace Web.Models
{
    public class Appointment
    {
           
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
 
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
        
        public string ShowAppointments(string Date)
        {
            string sURL;
            sURL = "https://localhost:5001/api/appointment/"+Date;

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            var result="";
            string sLine = "";
            int i = 0;

            while (sLine!=null)
            {
            i++;
            sLine = objReader.ReadLine();
                if (sLine!=null)
                {
                List<AppointmentEntity> appList= JsonConvert.DeserializeObject<List<AppointmentEntity>>(sLine);
                result="<table style=\"width:300px\"><tr><th>Reserved Dates</th><th>Patients</th></tr>";
                foreach(AppointmentEntity appObj in appList)
                {
                    result+="<tr><td>"+appObj.Date+"</td><td>"+appObj.PacientName+"</td></tr>";
                }      
                result+="</table>";
                }
            }
            return result;
        }
         public string ShowDates(string Date)
        {
            string sURL;
            sURL = "https://localhost:5001/api/appointment/"+Date;

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            var result="";
            string sLine = "";
            int i = 0;

            while (sLine!=null)
            {
            i++;
            sLine = objReader.ReadLine();
                if (sLine!=null)
                {
                List<AppointmentEntity> appList= JsonConvert.DeserializeObject<List<AppointmentEntity>>(sLine);
                result="<table style=\"width:300px\"><tr><th>Reserved Dates for "+Date +"</th></tr>";
                foreach(AppointmentEntity appObj in appList)
                {
                    result+="<tr><td>"+appObj.Date+"</td></tr>";
                }      
                result+="</table>";
                }
            }
            return result;
        }
        public bool AddAppointment(string medName,string d,string patientName,string breedPet,string symptoms)
        {
           
           string sURL;
           sURL = "https://localhost:5001/api/appointment";

            WebRequest wrPOSTURL;
            wrPOSTURL = WebRequest.Create(sURL);
            wrPOSTURL.ContentType = "application/json";
            wrPOSTURL.Method = "POST";

            using (var streamWriter = new StreamWriter(wrPOSTURL.GetRequestStream()))
            {
                string json = "{\"medName\": \""+ medName + "\","+"\"date\": \""+d+ "\","+"\"pacientName\": \""+ patientName + "\","+"\"breedPet\": \""+ breedPet + "\","+"\"symptoms\": \""+ symptoms + "\"}"; 
                wrPOSTURL.ContentLength = json.Length;
                streamWriter.Write(json);
            }
            
            try
            {
                var httpResponse = (HttpWebResponse)wrPOSTURL.GetResponse();
            
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            return true;
            }
            catch{
                return false;
            }

        }
    }
}

        


