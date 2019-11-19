using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook_MVC.Models.UserComponents;

namespace CookBook_MVC.Repositories
{
    interface IRecipeRepository
    {
        Task Create(Recipe recipe);
        Task<List<Recipe>> GetRecipesAsync();

    }
}
