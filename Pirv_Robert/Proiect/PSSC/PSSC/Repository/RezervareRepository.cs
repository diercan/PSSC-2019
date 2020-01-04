using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSSC.Models;
namespace PSSC.Repository
{
    public interface IRezervareRepository
    {
        void CreareRezervare(Rezervare rez);

        List<Rezervare> ObtineRezervari();

        Rezervare ObtineRezervareDupaGuid(Guid id);

        void StergeRezervare(Rezervare rez);
    }

    public class RezervareRepository : IRezervareRepository
    {
        private List<Rezervare> Lista;
        public RezervareRepository()
        {
            Lista = new List<Rezervare>();
            Lista.Add(new Rezervare
            {
                IdUnic = Guid.NewGuid(),
                data = DateTime.Now,
                Nume = "Pirv",
                Prenume = "Robert",
                murdarie = stareMasina.foarte_murdara,
                optiune1=optiuni.ceara,
                optiune2=optiuni.exterior,
                optiune3=optiuni.interior,
                optiune4=optiuni.portbagaj
            }) ;
        }
        public void CreareRezervare(Rezervare rez)
        {
            Lista.Add(rez);
        }

        public Rezervare ObtineRezervareDupaGuid(Guid id)
        {
            return Lista.FirstOrDefault(var => var.IdUnic == id);
        }

        public List<Rezervare> ObtineRezervari()
        {
            return Lista;
        }

        public void StergeRezervare(Rezervare rez)
        {
            Lista.Remove(rez);
        }
    }
}