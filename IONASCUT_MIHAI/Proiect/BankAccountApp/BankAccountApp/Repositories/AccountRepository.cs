using System.Threading.Tasks;
using BankAccountApp.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace BankAccountApp.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task AddAccountTask(AccountEntity account)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("accountpssc");
            await table.CreateIfNotExistsAsync();

            var insertOrReplaceOperation = TableOperation.InsertOrReplace(account);
            await table.ExecuteAsync(insertOrReplaceOperation);
        }

        public async Task<AccountEntity> GetAccountTask(string IBAN)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("accountpssc");
            await table.CreateIfNotExistsAsync();

            TableQuery<AccountEntity> query = new TableQuery<AccountEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, IBAN));
            TableContinuationToken token = null;
            TableQuerySegment<AccountEntity> resultSegment = await table.ExecuteQuerySegmentedAsync(query, token);
            token = resultSegment.ContinuationToken;

            foreach(AccountEntity entity in resultSegment.Results)
            {
                return entity;
            }

            return null;
        }

        public async Task AddMoneyTask(AccountEntity account, double sum)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("accountpssc");
            await table.CreateIfNotExistsAsync();

            account.Balance += sum;
            var insertOrReplaceOperation = TableOperation.InsertOrReplace(account);
            await table.ExecuteAsync(insertOrReplaceOperation);
        }

        public async Task RetrieveMoneyTask(AccountEntity account, double sum)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("accountpssc");
            await table.CreateIfNotExistsAsync();

            account.Balance -= sum;
            var insertOrReplaceOperation = TableOperation.InsertOrReplace(account);
            await table.ExecuteAsync(insertOrReplaceOperation);
        }

        public async Task TransferMoneyTask(AccountEntity accountSource, AccountEntity accountDestination, double sum)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storeksq6lvimcn6gy;AccountKey=okS17G921c6N5lN7Czxi1QJ+DF/fbripzaWiEDoRdp4oh42RoJFz3A5Nfn70dHaoh3mUaFzQIcu9MVDTeHmmiQ==;EndpointSuffix=core.windows.net");

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference("accountpssc");
            await table.CreateIfNotExistsAsync();

            accountSource.Balance -= sum;
            var insertOrReplaceOperation = TableOperation.InsertOrReplace(accountSource);
            await table.ExecuteAsync(insertOrReplaceOperation);
            accountDestination.Balance += sum;
            insertOrReplaceOperation = TableOperation.InsertOrReplace(accountDestination);
            await table.ExecuteAsync(insertOrReplaceOperation);
        }
    }
}
