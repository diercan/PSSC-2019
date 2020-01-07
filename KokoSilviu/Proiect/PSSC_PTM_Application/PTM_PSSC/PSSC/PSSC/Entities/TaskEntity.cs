using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSSC;
using Microsoft.WindowsAzure.Storage.Table;

namespace PSSC.Entities
{
    public class TaskEntity:TableEntity
    {
        public string author { get;  set; }
        public string developer { get; set; }
        public string description { get;  set; }
        public string status { get;  set; }
        public string prio { get;  set; }

        public TaskEntity()
        {
        }
        public TaskEntity(PSSC.Models.Task task)
        {
            PartitionKey = task.id.ToString();
            RowKey = task.name;
            author = task.author.internal_id;
            developer = task.developer.internal_id;
            status = task.status;
            description = task.description;
            prio = task.priority;          
        }

        public TaskEntity(string id, string name)
        {
            PartitionKey = id;
            RowKey = name;
        }
    }
}
