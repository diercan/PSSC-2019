using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook_MVC.Models.CommonComponents;

namespace CookBook_MVC.Models.UserComponents.RecipeComponents
{
    public class Ingredients
    {
        private Text IngredientName;
        private Quantity quantity;
        public Ingredients(Text name, Quantity qty)
        {
            IngredientName = name;
            quantity = qty;
        }
        public Ingredients GetIngredient { get { return this; } }
        public Text GetIngredientName { get { return IngredientName; } }
        public Quantity GetQuantity { get { return quantity; } }
    }
}
