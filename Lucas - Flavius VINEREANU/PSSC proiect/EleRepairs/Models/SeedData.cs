using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EleRepairs.Data;
using System;
using System.Linq;

namespace EleRepairs.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EleRepairsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EleRepairsContext>>()))
            {
                // Look for any movies.
                if (context.Repairs.Any())
                {
                    return;   // DB has been seeded
                }

                context.Repairs.AddRange(
                    new Repairs
                    {
                        Title = "When Samsung Met Apple",
                        ReceiveDate = DateTime.Parse("2020-2-10"),
                        ReleaseDate = DateTime.Parse("2020-2-12 12:00"),
                        Genre = "Phone",
                        Rating = "C",
                        Price = 70.9M
                    },

                    new Repairs
                    {
                        Title = "Ghostbusters ",
                        ReceiveDate = DateTime.Parse("2020-1-10"),
                        ReleaseDate = DateTime.Parse("2020-1-13 12:00"),
                        Genre = "TV",
                        Rating = "B",
                        Price = 80.99M
                    },

                    new Repairs
                    {
                        Title = "Ghostbusters 2",
                        ReceiveDate = DateTime.Parse("2020-1-14"),
                        ReleaseDate = DateTime.Parse("2020-1-16 12:00"),
                        Genre = "TV",
                        Rating = "B",
                        Price = 80.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}