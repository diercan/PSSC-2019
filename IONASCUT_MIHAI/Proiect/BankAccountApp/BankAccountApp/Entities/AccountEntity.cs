using Microsoft.WindowsAzure.Storage.Table;

namespace BankAccountApp.Entities
{
    public class AccountEntity : TableEntity
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public string ExpiryDate { get; set; }

        public AccountEntity()
        {
        }

        public AccountEntity(string IBAN, string surname)
        {
            PartitionKey = IBAN;
            RowKey = surname;
        }
    }
}
