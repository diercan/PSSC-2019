using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{

    public class AppPhoneDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppPhoneDBContext(DbContextOptions<AppPhoneDBContext> options)
            : base(options)
        {

        }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
 