using Microsoft.WindowsAzure.Storage.Table;

namespace Web.Models
{
    public class MedEntity : TableEntity
    {
        public int Id{ get; set;}
        // public string Name{ get; set; }
        
        public string Specialty { get; set; }   
        // public string Password { get; set; }  
      
        public MedEntity(string username,string password) { 
            this.PartitionKey = username;
            this.RowKey = password;
        }
        public MedEntity(){}
    }
}