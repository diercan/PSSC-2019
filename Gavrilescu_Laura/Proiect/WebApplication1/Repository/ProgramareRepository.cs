using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Repository
{
    public interface IProgramareRepository
    {
        void  CreateReservation(Programare reservation);
        List<Programare> GetAllReservations();
        Programare GetReservationById(Guid id);
        void DeleteReservation(Programare reservation);
    }

   public class ProgramareRepository : IProgramareRepository
    {
        private readonly List<Programare> List;

        public ProgramareRepository()
        {
            List = new List<Programare>();
            List.Add(new Programare
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "Laura",
                Washtype = WashType.Canapea
            });
            List.Add(new Programare
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "Laura",
                Washtype = WashType.Saltea
            });
            List.Add(new Programare
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "Laura",
                Washtype = WashType.Masina
            });
        }

        public void CreateReservation(Programare reservation)
        {
            List.Add(reservation);
        }

        public void DeleteReservation(Programare reservation)
        {
            List.Remove(reservation);
        }

        public List<Programare> GetAllReservations()
        {
            return List;
        }

        public Programare GetReservationById(Guid id)
        {
            return List.FirstOrDefault(_ => _.Id == id);
        }
    }
}