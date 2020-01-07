using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeTestBackend.Models;

namespace WeTestBackend.Repositories
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { 
        
                
        }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Tester> Testers { get; set; }
    }
}
