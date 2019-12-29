using Microsoft.WindowsAzure.Storage.Table;

namespace GestiuneElevi.Models
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

        public string getInfo()
        {
            return Nume + "\t" + Prenume + "\t" + Varsta.ToString() + "\t" + Clasa.ToString();
        }

        public string getInfo(string materie, int nota)
        {
            return Nume + "\t" + Prenume + "\t" + Clasa.ToString() + "\t" + materie + "\t" + nota.ToString();
        }

        public override string ToString()
        {
            return PartitionKey + "," + RowKey + "," + Nume + "," + Prenume + "," + Varsta.ToString() + "," + Clasa.ToString();
        }
    }
}