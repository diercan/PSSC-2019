using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MvcExample.Models
{
    public class MvcExampleContext : DbContext
    {
        public MvcExampleContext (DbContextOptions<MvcExampleContext> options)
            : base(options)
        {
        }

        public DbSet<MvcExample.Models.StudentMed> StudentMed { get; set; }
    }
}
