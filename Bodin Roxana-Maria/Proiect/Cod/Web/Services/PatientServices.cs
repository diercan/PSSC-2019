using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Web.Models;

namespace Web.Services
{
    public class PatientService : IPatientService, IDisposable
    {
        private CloudTable Patient;
        

        public PatientService()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=vetclinic;AccountKey=Ml3e+excdJIjp81hyF7ANCT3PV3AOdI4xiZ8gM4xlAs5MHLd4EV4lFi5gyvw/6UWtMbs0djuAcDGYZFTzX6xiQ==;EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            Patient = tableClient.GetTableReference("Patient");

        }

        public async Task Initialize()
        {
            await Patient.CreateIfNotExistsAsync();
        }

        public async Task<List<PatientEntity>> GetPatients()
        {
            if (Patient == null)
            {
                throw new Exception();
            }
            
            var patients = new List<PatientEntity>();
            TableQuery<PatientEntity> query = new TableQuery<PatientEntity>(); //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<PatientEntity> resultSegment = await Patient.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                patients.AddRange(resultSegment.Results);
            } while (token != null);

            return patients;
        }
        public async Task<List<PatientEntity>> GetPatientByUserName(string username)
        {
            if (Patient == null)
            {
                throw new Exception();
            }
            
            var patients = new List<PatientEntity>();
            TableQuery<PatientEntity> query = new TableQuery<PatientEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, username));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<PatientEntity> resultSegment = await Patient.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                patients.AddRange(resultSegment.Results);
            } while (token != null);

            return patients;
        }
        public async Task<TableResult> AddPatient(PatientEntity patient)
        {
            if (Patient == null)
            {
                throw new Exception();
            }
	
            var insertOperation = TableOperation.Insert(patient);
            return await Patient.ExecuteAsync(insertOperation);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
