using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using MVC_CookBook_PSSC.Models;
using MVC_CookBook_PSSC.Models.Exceptions;
using MVC_CookBook_PSSC.Models.UserComponents;
using MVC_CookBook_PSSC.Models.UserComponents.RecipeComponents;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBookTests.Tests
{
    public class TestRecipeModelMethods
    {
        public object Ingredient { get; private set; }

        [Test]
        public async Task TestCreateCorrectObject()
        {
            Recipe recipe = new Recipe();
            Dictionary<String, StringValues> dictionary = new Dictionary<string, StringValues>()
            {
          
                ["strUserID"] = "1",
                ["strRecipeName"] = "Sarmale",
                ["strRawIngredients"] = @"carne 100 gr
varza 5 felii
piper 2 gr",
                ["strPreparation"] = "Faci sarmalele",
                ["strCreatorUsername"] = "Usernam",
                
            };
            FormCollection form = new FormCollection(dictionary);
            var user = new User()
            {
                strUsername = "Usernam"
            };
            await recipe.CreateValueObjectRecipeAsync(form, user,"root");
            recipe.CreateDatabaseObject();

            Assert.AreEqual(recipe.GetRecipeName.GetText, "Sarmale");
            Assert.AreEqual(recipe.strCreatorUsername, user.strUsername);
        }

        [Test]
        public void TestAddingSameIngredientsToUniqueList()
        {
            Recipe recipe = new Recipe();
            Ingredients ing1 = new Ingredients(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt"), new Quantity(new MVC_CookBook_PSSC.Models.CommonComponents.Number(10), new MeasuringUnit(new MVC_CookBook_PSSC.Models.CommonComponents.ShortText("txt"))));
            Ingredients ing2 = new Ingredients(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt"), new Quantity(new MVC_CookBook_PSSC.Models.CommonComponents.Number(10), new MeasuringUnit(new MVC_CookBook_PSSC.Models.CommonComponents.ShortText("txt"))));
            Ingredients ing3 = new Ingredients(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt2"), new Quantity(new MVC_CookBook_PSSC.Models.CommonComponents.Number(10), new MeasuringUnit(new MVC_CookBook_PSSC.Models.CommonComponents.ShortText("txt"))));

            recipe.AddInexistentIngredient(ing1);
            recipe.AddInexistentIngredient(ing2);
            Assert.AreNotSame(ing1, ing2);
            Assert.AreEqual(1, recipe.GetIngredients.Count);

            recipe.AddInexistentIngredient(ing3);
            Assert.AreNotSame(ing1, ing3);
            Assert.AreEqual(2, recipe.GetIngredients.Count);
        }

        [Test]
        public void TestRemoveIngredientNoThrow()
        {
            Recipe recipe = new Recipe();
            Ingredients ing1 = new Ingredients(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt"), new Quantity(new MVC_CookBook_PSSC.Models.CommonComponents.Number(10), new MeasuringUnit(new MVC_CookBook_PSSC.Models.CommonComponents.ShortText("txt"))));
           
            recipe.AddInexistentIngredient(ing1);
            Assert.AreEqual(1, recipe.GetIngredients.Count);
            recipe.RemoveIngredient(ing1);
            Assert.AreEqual(0, recipe.GetIngredients.Count);

        }
        [Test]
        public void TestRemoveIngredientThrow()
        {
            Recipe recipe = new Recipe();
            Ingredients ing1 = new Ingredients(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt"), new Quantity(new MVC_CookBook_PSSC.Models.CommonComponents.Number(10), new MeasuringUnit(new MVC_CookBook_PSSC.Models.CommonComponents.ShortText("txt"))));
            Ingredients ing2 = new Ingredients(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt3"), new Quantity(new MVC_CookBook_PSSC.Models.CommonComponents.Number(10), new MeasuringUnit(new MVC_CookBook_PSSC.Models.CommonComponents.ShortText("txt"))));

            recipe.AddInexistentIngredient(ing1);
            Assert.AreEqual(1, recipe.GetIngredients.Count);
            Assert.Throws<InexistentIngredientException>(() => recipe.RemoveIngredient(ing2));
            

        }
        [Test]
        public void AddIngredientsListNoThrow()
        {
            Recipe recipe = new Recipe();
            Ingredients ing1 = new Ingredients(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt"), new Quantity(new MVC_CookBook_PSSC.Models.CommonComponents.Number(10), new MeasuringUnit(new MVC_CookBook_PSSC.Models.CommonComponents.ShortText("txt"))));
            Ingredients ing2 = new Ingredients(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt3"), new Quantity(new MVC_CookBook_PSSC.Models.CommonComponents.Number(10), new MeasuringUnit(new MVC_CookBook_PSSC.Models.CommonComponents.ShortText("txt"))));
            
            Assert.DoesNotThrow(()=> recipe.AddIngredients(new List<Ingredients> { ing1, ing2 }));
        }

        [Test]
        public void GetIngredientsWithExistentIngredient()
        {
            Recipe recipe = new Recipe();
            Ingredients ing1 = new Ingredients(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt"), new Quantity(new MVC_CookBook_PSSC.Models.CommonComponents.Number(10), new MeasuringUnit(new MVC_CookBook_PSSC.Models.CommonComponents.ShortText("txt"))));
            recipe.AddInexistentIngredient(ing1);


            var returned = recipe.GetSpecificIngredient(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt"));

            Assert.IsInstanceOf<Ingredients>(returned);
            Assert.IsNotNull(returned);
        }

        [Test]
        public void GetIngredientsWithInexistentIngredient()
        {
            Recipe recipe = new Recipe();
            Ingredients ing1 = new Ingredients(new MVC_CookBook_PSSC.Models.CommonComponents.Text("txt"), new Quantity(new MVC_CookBook_PSSC.Models.CommonComponents.Number(10), new MeasuringUnit(new MVC_CookBook_PSSC.Models.CommonComponents.ShortText("txt"))));
            recipe.AddInexistentIngredient(ing1);


            var returned = recipe.GetSpecificIngredient(new MVC_CookBook_PSSC.Models.CommonComponents.Text("nope"));

            Assert.IsNull(returned);
        }

    }
}
