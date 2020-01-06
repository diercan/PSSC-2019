using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPlanner.Models;

namespace MyPlanner.Data
{
    public class MyTaskContext : DbContext
    {
        public MyTaskContext(DbContextOptions<MyTaskContext> options)
            : base(options)
        {
        }

        public DbSet<MyTask> MyTask { get; set; }
    }
}
