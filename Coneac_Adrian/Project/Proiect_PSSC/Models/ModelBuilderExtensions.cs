using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_PSSC.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacient>().HasData(
               new Pacient
               {
                   Id = 1,
                   Nume = "Adi",
                   Prenume = "Ion",
                   CNP = "12431231412",
                   Sexul = Sex.Masculin,
                   Adresa = "Sacalaz",
               }
               );
        }
    }
}
