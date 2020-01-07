using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;


namespace PSSC
{
    class UserEntity : TableEntity
    {
        public string password { get; private set; }
        public string role { get; private set; }
  

        public UserEntity()
        {
        }

        public UserEntity(string id, string uid)
        {
            PartitionKey = id;
            RowKey = uid;
        }
    }
}
