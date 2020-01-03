using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeTest.Models;

namespace WeTest.Data
{
    public class WeTestContext : DbContext
    {
        public WeTestContext (DbContextOptions<WeTestContext> options)
            : base(options)
        {
        }

        public DbSet<WeTest.Models.Test> Test { get; set; }
    }
}
