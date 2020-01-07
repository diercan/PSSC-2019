using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.Atributes
{
    public class DistribuitorDbContext : DbContext
    {
        public DistribuitorDbContext()
        {

        }
        
        public DistribuitorDbContext(DbContextOptions<DistribuitorDbContext> options) : base(options)
        {

        }
        public Microsoft.EntityFrameworkCore.DbSet<Distribuitor> distribuitori { get; set; }
    }
}
