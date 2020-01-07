using Microsoft.EntityFrameworkCore;
using MVC_CookBook_PSSC.Models.UserComponents.RecipeComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Models.UserComponents
{
    public class RecipeDbContext:DbContext
    {
        public RecipeDbContext()
        {

        }
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options) { }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ingredients>().HasNoKey();
        }
    }
}
