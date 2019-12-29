using Microsoft.WindowsAzure.Storage.Table;

namespace GestiuneElevi.Entities
{
    public class ElevEntity : TableEntity
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public int Varsta { get; set; }
        public int Clasa { get; set; }

        public ElevEntity()
        {
        }

        public ElevEntity(string id, string cnp)
        {
            PartitionKey = id;
            RowKey = cnp;
        }
    }
}