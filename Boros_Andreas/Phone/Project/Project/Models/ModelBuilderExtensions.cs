using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>().HasData(
               new Phone
               {
                   Id = 1,
                   Name = "Iphone X",
                   Color = "Silver",
                   Dimension = "10x20x30",
                   Type = "XS MAX",
               }
               );
        }
    }
}
