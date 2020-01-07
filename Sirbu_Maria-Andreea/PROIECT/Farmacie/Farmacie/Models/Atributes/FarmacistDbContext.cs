using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models
{
    public class FarmacistDbContext : DbContext
    {
        public FarmacistDbContext()
        {

        }

        public FarmacistDbContext(DbContextOptions<FarmacistDbContext> options) : base(options)
        {

        }
        public Microsoft.EntityFrameworkCore.DbSet<Farmacist> farmacisti { get; set; }
    }
}
