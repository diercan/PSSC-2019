using PSSC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSSC;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using PSSC.Entities;

namespace PSSC.Repositories
{
    public class TaskRepository : ITaskRepository
    {
     
        public async Task Create(TaskEntity task)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("PSSCTasksKoko");
            await table.CreateIfNotExistsAsync();

            var insertOrReplaceOperation = TableOperation.InsertOrReplace(task);
            await table.ExecuteAsync(insertOrReplaceOperation);

        }

        public async Task<List<TaskEntity>> GetAllTasks()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("PSSCLogInKoko");
           await table.CreateIfNotExistsAsync();

            List<TaskEntity> users = new List<TaskEntity>();
            TableQuery<TaskEntity> query = new TableQuery<TaskEntity>();
            users = table.ExecuteQuery(new TableQuery<TaskEntity>()).ToList();

            if (users.Count > 0)
                return users;
            else
                return null;

        }
        public async Task<TaskEntity> GetTask(int taskID)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("PSSCTasksKoko");
            await table.CreateIfNotExistsAsync();

            TableQuery<TaskEntity> query = new TableQuery<TaskEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, taskID.ToString()));
            TableContinuationToken token = null;
            TableQuerySegment<TaskEntity> resultSegment = await table.ExecuteQuerySegmentedAsync(query, token);
            token = resultSegment.ContinuationToken;

            foreach (TaskEntity entity in resultSegment.Results)
            {
                if(int.Parse(entity.PartitionKey)==taskID)
                      return entity;
            }
           
            return null;

        }
        public async Task UpdateTaskStatus(TaskEntity new_task)
        {
            TaskEntity old_task = await GetTask(int.Parse(new_task.PartitionKey));

            if (old_task.status != new_task.status)
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

                // Create a table client for interacting with the table service
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                // Create a table client for interacting with the table service 
                CloudTable table = tableClient.GetTableReference("PSSCTasksKoko");
                await table.CreateIfNotExistsAsync();

                var insertOrReplaceOperation = TableOperation.InsertOrReplace(new_task);
                await table.ExecuteAsync(insertOrReplaceOperation);
            }
        }

        public async Task UpdateTask(TaskEntity new_task)
        {
            bool task_changed = false;
            TaskEntity old_task = await GetTask(int.Parse(new_task.PartitionKey));

            if (old_task.status != new_task.status)
            {
                old_task.status = new_task.status;
                task_changed = true;          
            }

            if (old_task.RowKey != new_task.RowKey)
            {
                old_task.RowKey = new_task.RowKey;
                task_changed = true;
            }

            if (old_task.PartitionKey != new_task.PartitionKey)
            {
                old_task.PartitionKey = new_task.PartitionKey;
                task_changed = true;
            }

            if (old_task.description != new_task.description)
            {
                old_task.description = new_task.description;
                task_changed = true;
            }

            if (old_task.prio != new_task.prio)
            {
                old_task.prio = new_task.prio;
                task_changed = true;
            }

            if (old_task.developer != new_task.developer)
            {
                old_task.developer = new_task.developer;
                task_changed = true;
            }

            if (task_changed)
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

                // Create a table client for interacting with the table service
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                // Create a table client for interacting with the table service 
                CloudTable table = tableClient.GetTableReference("PSSCTasksKoko");
                await table.CreateIfNotExistsAsync();

                var insertOrReplaceOperation = TableOperation.InsertOrReplace(old_task);
                await table.ExecuteAsync(insertOrReplaceOperation);
            }
            
        }

        public async Task DeleteTask(TaskEntity task)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("PSSCTasksKoko");
            await table.CreateIfNotExistsAsync();

            var deleteOperation = TableOperation.Delete(task);
            await table.ExecuteAsync(deleteOperation);
        }

        public async Task<int> GetPlannedNr(UserLogInEntity dev, bool pm_user)
        {
            int cnt = 0;
            List<TaskEntity> tasks = await GetAllTasks();
            foreach(TaskEntity task in tasks)
            {
                if(task.status=="Planned")
                {
                    if (task.developer == dev.PartitionKey && !pm_user)
                        cnt++;
                    else if (pm_user)
                        cnt++;
                }
            }
            return cnt;
        }
        public async Task<int> GetInWorkN(UserLogInEntity dev, bool pm_user)
        {
            int cnt = 0;
            List<TaskEntity> tasks = await GetAllTasks();
            foreach (TaskEntity task in tasks)
            {
                if (task.status == "InWork")
                {
                    if (task.developer == dev.PartitionKey && !pm_user)
                        cnt++;
                    else if (pm_user)
                        cnt++;
                }
            }
            return cnt;
        }
        public async Task<int> GetRealizedNr(UserLogInEntity dev, bool pm_user)
        {
            int cnt = 0;
            List<TaskEntity> tasks = await GetAllTasks();
            foreach (TaskEntity task in tasks)
            {
                if (task.status == "Realized")
                {
                    if (task.developer == dev.PartitionKey && !pm_user)
                        cnt++;
                    else if (pm_user)
                        cnt++;
                }
            }
            return cnt;
        }
        public async Task<int> GetCanceledNr(UserLogInEntity dev, bool pm_user)
        {
            int cnt = 0;
            List<TaskEntity> tasks = await GetAllTasks();
            foreach (TaskEntity task in tasks)
            {
                if (task.status == "Canceled")
                {
                    if (task.developer == dev.PartitionKey && !pm_user)
                        cnt++;
                    else if (pm_user)
                        cnt++;
                }
            }
            return cnt;
        }
        public async Task AddDeveloper(UserLogInEntity dev)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("PSSCLogInKoko");
            await table.CreateIfNotExistsAsync();

            var insertOrReplaceOperation = TableOperation.InsertOrReplace(dev);
            await table.ExecuteAsync(insertOrReplaceOperation);
        }

        public async Task DeleteDeveloper(UserLogInEntity dev)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("PSSCLogInKoko");
            await table.CreateIfNotExistsAsync();

            var deleteOperation = TableOperation.Delete(dev);
            await table.ExecuteAsync(deleteOperation);
        }
    }
}
