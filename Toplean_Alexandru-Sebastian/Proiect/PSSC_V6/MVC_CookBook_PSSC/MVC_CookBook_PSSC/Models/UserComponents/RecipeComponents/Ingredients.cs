using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MVC_CookBook_PSSC.Models.CommonComponents;

namespace MVC_CookBook_PSSC.Models.UserComponents.RecipeComponents
{
    public class Ingredients
    {
        private int _id;
        private Text IngredientName;
        private Quantity quantity;
        public Ingredients()
        {
                
        }
        public Ingredients(Text name, Quantity qty)
        {
            IngredientName = name;
            quantity = qty;
        }
        [Key]
        public int ID { get; set; }
        public Ingredients GetIngredient { get { return this; } }
        public Text GetIngredientName { get { return IngredientName; } }
        public Quantity GetQuantity { get { return quantity; } }
        public Text GetIngredientAsText { get
            {
                return new Text(IngredientName.GetText + "#" + quantity.GetNumber.GetNumber + "#" + quantity.GetMeasuringUnit.GetText.GetText);

            } }
    }
}
