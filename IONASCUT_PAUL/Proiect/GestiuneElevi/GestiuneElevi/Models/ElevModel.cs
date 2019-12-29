namespace GestiuneElevi.Models
{
    public class ElevModel
    {
        public string Id { get; private set; }
        public string Cnp { get; private set; }
        public string Nume { get; private set; }
        public string Prenume { get; private set; }
        public int Varsta { get; private set; }
        public int Clasa { get; private set; }

        public void setId(string id)
        {
            Id = id;
        }

        public void setCnp(string cnp)
        {
            Cnp = cnp;
        }

        public void setNume(string nume)
        {
            Nume = nume;
        }

        public void setPrenume(string prenume)
        {
            Prenume = prenume;
        }

        public void setVarsta(int varsta)
        {
            Varsta = varsta;
        }

        public void setClasa(int clasa)
        {
            Clasa = clasa;
        }

        public string getInfo()
        {
            return Nume + "\t" + Prenume + "\t" + Varsta.ToString() + "\t" + Clasa.ToString();
        }

        public string getInfo(string materie, int nota)
        {
            return Nume + "\t" + Prenume + "\t" + Clasa.ToString() + "\t" + materie + "\t" + nota.ToString();
        }
    }
}
