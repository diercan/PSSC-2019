using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Web.Models;

namespace Web.Services
{
    public class MedService : IMedService,IDisposable
    {
        private CloudTable Med;
        

        public MedService()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=vetclinic;AccountKey=Ml3e+excdJIjp81hyF7ANCT3PV3AOdI4xiZ8gM4xlAs5MHLd4EV4lFi5gyvw/6UWtMbs0djuAcDGYZFTzX6xiQ==;EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            Med = tableClient.GetTableReference("Med");

        }

        public async Task Initialize()
        {
            await Med.CreateIfNotExistsAsync();
        }

        public async Task<List<MedEntity>> GetMeds()
        {
            if (Med == null)
            {
                throw new Exception();
            }
             //MedEntity med=new MedEntity("vladcodrean","medicina"){ Specialty="Chirurgie Generala"};
             //await AddMed(med);
            var meds = new List<MedEntity>();
            TableQuery<MedEntity> query = new TableQuery<MedEntity>(); //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<MedEntity> resultSegment = await Med.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                meds.AddRange(resultSegment.Results);
            } while (token != null);

            return meds;
        }
            public async Task<List<MedEntity>> GetMedByUserName(string username)
        {
            if (Med == null)
            {
                throw new Exception();
            }
            
            var meds = new List<MedEntity>();
            TableQuery<MedEntity> query = new TableQuery<MedEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, username));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<MedEntity> resultSegment = await Med.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                meds.AddRange(resultSegment.Results);
            } while (token != null);

            return meds;
        }
         public async Task<TableResult> AddMed(MedEntity med)
        {
            if (Med == null)
            {
                throw new Exception();
            }
	
            var insertOperation = TableOperation.Insert(med);
            return await Med.ExecuteAsync(insertOperation);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
