using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSSC;
using Microsoft.WindowsAzure.Storage.Table;

namespace PSSC.Entities
{
    public class UserLogInEntity : TableEntity
    {
        public string role { get; set; }
 
        public UserLogInEntity()
        {
        }

        public UserLogInEntity(string uid, string pasword,string role)
        {
            PartitionKey = uid;
            RowKey = pasword;
            this.role = role;
        }

        public UserLogInEntity(string uid, string pasword)
        {
           PartitionKey = uid;
           RowKey = pasword;
        }
    }
}
