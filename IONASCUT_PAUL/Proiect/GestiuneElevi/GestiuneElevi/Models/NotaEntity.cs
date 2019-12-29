using Microsoft.WindowsAzure.Storage.Table;

namespace GestiuneElevi.Models
{
    public class NotaEntity : TableEntity
    {
        public string Materie { get; set; }
        public int Nota { get; set; }

        public NotaEntity()
        {
        }

        public NotaEntity(string id, string elevCNP)
        {
            PartitionKey = id;
            RowKey = elevCNP;
        }
    }
}
