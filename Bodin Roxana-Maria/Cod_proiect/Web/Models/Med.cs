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
    public class Med
    {
        
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
 
        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }
        public bool  IsValid(string _name, string _password)
        {
           
           string sURL;
           sURL = "https://localhost:5001/api/med";

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
                List<MedEntity> medList= JsonConvert.DeserializeObject<List<MedEntity>>(sLine);
                foreach(MedEntity medObj in medList)
                {
                    if(medObj.Name==_name && medObj.Password==_password)
                        return true;
                }
                    return false;
                }
                
            }
            return false;
        }
        public List<MedEntity> ShowMeds()
        {
           List<MedEntity> medList=new List<MedEntity>();
           string sURL;
           sURL = "https://localhost:5001/api/med";

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
                    medList= JsonConvert.DeserializeObject<List<MedEntity>>(sLine);
                }
            }
            return medList;
        }
    }
}