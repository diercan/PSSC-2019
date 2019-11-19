using CookBook_MVC.Models.UserComponents;
using CookBook_MVC.Models.UserComponents.RecipeComponents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook_MVC.Repositories
{
    public class RecipeRepository:IRecipeRepository
    {
        private readonly RecipeDbContext _context;
        public RecipeRepository(RecipeDbContext recipeDbContext)
        {
            _context = recipeDbContext;
        }
        public async Task Create(Recipe recipe) {
            _context.Add(recipe);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }
    }
}
