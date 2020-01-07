using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Atributes
{
    public class MedicamentDbContext : DbContext
    {
        public MedicamentDbContext()
        {

        }

        public MedicamentDbContext(DbContextOptions<MedicamentDbContext> options) : base(options)
        {

        }
        public Microsoft.EntityFrameworkCore.DbSet<Medicament> medicamente { get; set; }
    }
}
