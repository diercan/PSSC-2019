using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook_MVC.Models.CommonComponents;
using CookBook_MVC.Models.UserComponents.RecipeComponents;
using CookBook_MVC.Models.Exceptions;

namespace CookBook_MVC.Models.UserComponents
{
    public class Recipe
    {
        private Text RecipeName;
        private Ingredients ingredients;
        private List<Ingredients> ingredientsList;
        public Recipe(Text RecipeName)
        {
            this.RecipeName = RecipeName;
        }
        public Recipe(Text RecipeName, Ingredients ingredient)
        {
            this.RecipeName = RecipeName;
            this.ingredients = ingredient;
        }
        public Recipe(Text RecipeName, List<Ingredients> ingredients)
        {
            this.RecipeName = RecipeName;
            this.ingredientsList = ingredients;
        }
        public void AddIngredient(Ingredients ingredient)
        {
            if (ingredientsList.Contains(ingredient))
                throw new ExistentIngredientException("Ingredient already exists in the recipe");
            else
                ingredientsList.Add(ingredient);

        }
        public void RemoveIngredient(Ingredients ingredient)
        {
            if (ingredientsList.Contains(ingredient))
            {
                ingredientsList.Remove(ingredient);
            }
            else
                throw new InexistentIngredientException("Ingredient does not exist in the current recipe!");
        }
        public void AddIngredients(Ingredients[] ingredients)
        {
            int i = 0;
            try
            {
                for (i = 0; i < ingredients.Length; i++)
                {
                    if (ingredientsList.Contains(ingredients[i]))
                    {
                        throw new ExistentIngredientException("This ingredient already exists in the recipe");

                    }
                    else
                    {
                        ingredientsList.Add(ingredients[i]);
                    }
                }
            }
            catch (ExistentIngredientException)
            {
                for (int j = 0; j < i; j++)
                {
                    ingredientsList.Remove(ingredients[j]);
                }
            }

        }

        public Text GetRecipeName { get { return RecipeName; } }
        public List<Ingredients> GetIngredient { get { return ingredientsList; } }
        public Ingredients GetSpecificIngredient(Text ingredientName)
        {
            foreach(Ingredients i in ingredientsList)
            {
                if (i.GetIngredientName == ingredientName)
                    return i;
            }
            return null;
        }
        
    }
}
