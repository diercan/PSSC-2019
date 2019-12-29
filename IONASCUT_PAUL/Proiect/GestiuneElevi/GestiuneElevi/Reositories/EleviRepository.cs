using System.Collections.Generic;
using System.Threading.Tasks;
using GestiuneElevi.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace GestiuneElevi.Reositories
{
    public class EleviRepository : IEleviRepository
    {
        public async Task AdaugaElevAsyncTask(ElevEntity elev)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("elevipssc");
            await table.CreateIfNotExistsAsync();

            var insertOrReplaceOperation = TableOperation.InsertOrReplace(elev);
            await table.ExecuteAsync(insertOrReplaceOperation);
        }

        public async Task<List<ElevEntity>> GetAllEleviAsyncTask()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("elevipssc");
            await table.CreateIfNotExistsAsync();

            List<ElevEntity> elevi = new List<ElevEntity>();
            TableQuery<ElevEntity> query = new TableQuery<ElevEntity>();
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<ElevEntity> resultSegment = await table.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                elevi.AddRange(resultSegment.Results);
            } while(token != null);
            
            return elevi;
        }

        public async Task<ElevEntity> GetElevAsyncTask(string cnp)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("elevipssc");
            await table.CreateIfNotExistsAsync();

            TableQuery<ElevEntity> query = new TableQuery<ElevEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, cnp));
            TableContinuationToken token = null;
            TableQuerySegment<ElevEntity> resultSegment = await table.ExecuteQuerySegmentedAsync(query, token);
            token = resultSegment.ContinuationToken;

            foreach(ElevEntity entity in resultSegment.Results)
            {
                return entity;
            }

            return null;
        }

        public async Task AdaugaNotaAsyncTask(NotaEntity nota)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("notepssc");
            await table.CreateIfNotExistsAsync();

            var insertOperation = TableOperation.Insert(nota);
            await table.ExecuteAsync(insertOperation);
        }

        public async Task<List<NotaEntity>> GetAllNoteAsyncTask()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("notepssc");
            await table.CreateIfNotExistsAsync();

            List<NotaEntity> note = new List<NotaEntity>();
            TableQuery<NotaEntity> query = new TableQuery<NotaEntity>();
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<NotaEntity> resultSegment = await table.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                note.AddRange(resultSegment.Results);
            } while(token != null);

            return note;
        }
    }
}
