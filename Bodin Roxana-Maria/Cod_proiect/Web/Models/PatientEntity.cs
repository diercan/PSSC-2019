using Microsoft.WindowsAzure.Storage.Table;

namespace Web.Models
{
    public class PatientEntity : TableEntity
    {
        public int Id{ get; set;}
        public string Name{ get; set; }
        public string UserName { get; set; }        
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
        public PatientEntity() { }
        

    }
}