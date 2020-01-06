using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyPlanner.Data;

namespace MyPlanner.Models
{
    public static class SeedData //used with Entity Framework
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyPlannerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MyPlannerContext>>()))
            {
                // Look for any movies.
                if (context.MyTask.Any())
                {
                    return;   // DB has been seeded
                }

                context.MyTask.AddRange(
                    new MyTask
                    {
                        Description = "Add some test data",
                        Due_Date = DateTime.Now,
                        Project = "MVC Project",
                        Owner = "me",
                        Asignee ="also me",
                        Status= Models.MyTask.StatusType.InProgress,
                        Review="Very well"
                    },

                     new MyTask
                     {
                         Description = "Add some more test data",
                         Due_Date = DateTime.Now,
                         Project = "MVC Project",
                         Owner = "not me this time",
                         Asignee = "still not me",
                         Status = Models.MyTask.StatusType.InProgress
                     }

                );
                context.SaveChanges();
            }
        }
    }
}
