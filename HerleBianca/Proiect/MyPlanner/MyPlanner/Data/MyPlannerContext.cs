using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPlanner.Models;

namespace MyPlanner.Data
{
    public class MyPlannerContext : DbContext
    {
        public MyPlannerContext (DbContextOptions<MyPlannerContext> options)
            : base(options)
        {
        }

        public DbSet<MyPlanner.Models.MyTask> MyTask { get; set; }
        public DbSet<MyPlanner.Models.User> User { get; set; }
    }
}
