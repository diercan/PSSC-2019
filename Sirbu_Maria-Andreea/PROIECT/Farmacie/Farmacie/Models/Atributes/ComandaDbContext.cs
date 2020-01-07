using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Atributes
{
    public class ComandaDbContext : DbContext
    {
        public ComandaDbContext()
        {

        }

        public ComandaDbContext(DbContextOptions<ComandaDbContext> options) : base(options)
        {

        }
        public Microsoft.EntityFrameworkCore.DbSet<Comanda> comenzi { get; set; }
    }
}
