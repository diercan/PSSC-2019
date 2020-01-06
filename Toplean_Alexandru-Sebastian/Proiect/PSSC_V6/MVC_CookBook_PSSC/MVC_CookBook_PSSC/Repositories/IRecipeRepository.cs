using Microsoft.EntityFrameworkCore;
using MVC_CookBook_PSSC.Models.UserComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Repositories
{
    public interface IRecipeRepository
    {
        Task CreateAsync(Recipe recipe);
        Task<List<Recipe>> GetRecipesToListAsync();
        Task<Recipe> GetRecipeAsync(int? id);
        Task DeleteAsync(Recipe user);
        bool RecipeExists(int id);
        Task UpdateRecipeAsync(Recipe recipe);
        DbSet<Recipe> GetRecipesAsDbSet();
    }
}
