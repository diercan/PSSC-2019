using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Web.Models.DDD;

namespace Web.Services
{
    public class AppointmentService : IAppointmentService, IDisposable
    {
        private CloudTable Appointment;
        

        public AppointmentService()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=vetclinic;AccountKey=Ml3e+excdJIjp81hyF7ANCT3PV3AOdI4xiZ8gM4xlAs5MHLd4EV4lFi5gyvw/6UWtMbs0djuAcDGYZFTzX6xiQ==;EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            Appointment = tableClient.GetTableReference("Appointment");

        }

        public async Task Initialize()
        {
            await Appointment.CreateIfNotExistsAsync();
        }

        public async Task<List<Appointment>> GetAppointment()
        {
            if (Appointment == null)
            {
                throw new Exception();
            }
            
            var appointment = new List<Appointment>();
            TableQuery<Appointment> query = new TableQuery<Appointment>(); //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<Appointment> resultSegment = await Appointment.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                appointment.AddRange(resultSegment.Results);
            } while (token != null);

            return appointment;
        }
        
        public async Task<List<Appointment>> GetAppointmentByMed(string medName)
        {
          if (Appointment == null)
            {
                throw new Exception();
            }
            
            var appointment = new List<Appointment>();
            TableQuery<Appointment> query = new TableQuery<Appointment>().Where(TableQuery.GenerateFilterCondition("MedName", QueryComparisons.Equal, medName));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<Appointment> resultSegment = await Appointment.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                appointment.AddRange(resultSegment.Results);
            } while (token != null);

            return appointment;
        }
        public async Task<List<Appointment>> GetAppointmentByPatient(string patientName)
        {
          if (Appointment == null)
            {
                throw new Exception();
            }
            
            var appointment = new List<Appointment>();
            TableQuery<Appointment> query = new TableQuery<Appointment>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, patientName));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<Appointment> resultSegment = await Appointment.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                appointment.AddRange(resultSegment.Results);
            } while (token != null);

            return appointment;
        }
          public async Task<List<Appointment>> GetAppointmentById(string id)
        {
          if (Appointment == null)
            {
                throw new Exception();
            }
            
            var appointment = new List<Appointment>();
            TableQuery<Appointment> query = new TableQuery<Appointment>().Where(TableQuery.GenerateFilterCondition("Id", QueryComparisons.Equal, id));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<Appointment> resultSegment = await Appointment.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                appointment.AddRange(resultSegment.Results);
            } while (token != null);

            return appointment;
        }
        public async Task<TableResult> UpdateAppointment(Appointment app)
        {
            TableOperation retrieve = TableOperation.Retrieve<Appointment>(app.PartitionKey, app.RowKey);

            TableResult result = await Appointment.ExecuteAsync(retrieve);

            Appointment item = (Appointment)result.Result;
            item=app;
            item.ETag = "*";

            TableOperation update = TableOperation.InsertOrReplace(item);

            return await Appointment.ExecuteAsync(update);
        }
            public async Task<TableResult> AddAppointment(Appointment app)
        {
            if (Appointment == null)
            {
                throw new Exception();
            }
            List<Appointment> list = await GetAppointment();
            app.Id=""+list.Count;
            var insertOperation = TableOperation.Insert(app);
            return await Appointment.ExecuteAsync(insertOperation);
        }
        public async void Delete(Appointment appointment)
        {
           // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<Appointment>(appointment.PartitionKey,appointment.RowKey);

            // Execute the operation.
            TableResult retrievedResult = await Appointment.ExecuteAsync(retrieveOperation);

            // Assign the result to a CustomerEntity object.
            Appointment deleteEntity = (Appointment)retrievedResult.Result;

            // Create the Delete TableOperation and then execute it.
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                 await Appointment.ExecuteAsync(deleteOperation);
                
            }
            else
            {
                 Console.WriteLine("Couldn't delete the entity.");
            }
           
            
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
