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
    public class Patient
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
 
        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }
        
        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public bool  IsValid(string _username, string _password)
        {
           
           string sURL;
           sURL = "https://localhost:5001/api/patient";

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine!=null)
            {
            i++;
            sLine = objReader.ReadLine();
                if (sLine!=null)
                {
                List<PatientEntity> patientList= JsonConvert.DeserializeObject<List<PatientEntity>>(sLine);
                foreach(PatientEntity patientObj in patientList)
                {
                    if(patientObj.UserName==_username && patientObj.Password==_password)
                        return true;
                }
                    return false;
                }
                
            }
            return false;
        }
    }
}
        


