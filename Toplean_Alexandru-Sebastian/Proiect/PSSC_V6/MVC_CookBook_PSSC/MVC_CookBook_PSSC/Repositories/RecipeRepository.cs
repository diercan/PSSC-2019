using Microsoft.EntityFrameworkCore;
using MVC_CookBook_PSSC.Models.UserComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Repositories
{
    public class RecipeRepository:IRecipeRepository
    {
        private readonly RecipeDbContext _context;
        public RecipeRepository(RecipeDbContext userDbContext)
        {
            _context = userDbContext;
        }
        public async Task CreateAsync(Recipe recipe)
        {
            _context.Add(recipe);
            await _context.SaveChangesAsync();

        }
        public async Task<List<Recipe>> GetRecipesToListAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<Recipe> GetRecipeAsync(int? id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async Task DeleteAsync(Recipe user)
        {
            _context.Recipes.Remove(user);
            await _context.SaveChangesAsync();
        }
        public bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.strID == id);
        }
        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            _context.Update(recipe);
            await _context.SaveChangesAsync();
        }
        public DbSet<Recipe> GetRecipesAsDbSet()
        {
            return _context.Recipes;
        }
    }

}
