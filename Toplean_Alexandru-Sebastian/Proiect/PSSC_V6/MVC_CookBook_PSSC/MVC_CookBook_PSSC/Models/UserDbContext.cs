using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_CookBook_PSSC.Models.UserComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MVC_CookBook_PSSC.Models.UserComponents.RecipeComponents;
using MVC_CookBook_PSSC.Models;

namespace MVC_CookBook_PSSC.Models
{
    public class UserDbContext:DbContext
    {
        public UserDbContext()
        {

        }
        
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<MVC_CookBook_PSSC.Models.UserComponents.RecipeComponents.Ingredients> Ingredients { get; set; }
        public DbSet<MVC_CookBook_PSSC.Models.UserLoginModel> UserLoginModel { get; set; }
       
        
    }
}
