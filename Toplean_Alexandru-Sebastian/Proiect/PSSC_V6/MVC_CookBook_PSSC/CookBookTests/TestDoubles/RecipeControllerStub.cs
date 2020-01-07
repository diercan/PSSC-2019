using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using MVC_CookBook_PSSC.Controllers;
using MVC_CookBook_PSSC.Models;
using MVC_CookBook_PSSC.Models.UserComponents;
using MVC_CookBook_PSSC.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBookTests.TestDoubles
{
    class RecipeControllerStub:RecipeController
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;
        




        public RecipeControllerStub(IRecipeRepository repo, IUserRepository userRepo) : base(repo, userRepo)
        {
            _recipeRepository = repo;
            _userRepository = userRepo;
        }
        public new async Task<IActionResult> Index()
        {
            
            return View(await _recipeRepository.GetRecipesToListAsync());
        }
        public new async Task<IActionResult> View(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var recipe = await _recipeRepository.GetRecipeAsync(id);
            // var creator = await _userRepository.GetUserAsync(recipe.strUserID);
           
            
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public new async Task<IActionResult> Create(IFormCollection files)
        {
            Recipe recipe = new Recipe();


            if (ModelState.IsValid)
            {

                String wwwroot = @"C:\Users\Seba\Desktop\PSSC_Unpublished-master\MVC_CookBook_PSSC\MVC_CookBook_PSSC\wwwroot\";
                var userConnected = new User() { strPassword = "Passw0rd", strUsername = "Usernam", strEmail = "email@email.email", strMoney = 0, strName = "name", strSurename = "surename", strPremium = false, strIsLogged = false, strIpAddress = ":::1", strCNP = "1234567890123", strCookie = "cookie", strIBAN = "ibanuuu" };

                string recipeName = files["strRecipeName"];



                if (await recipe.CreateValueObjectRecipeAsync(files, userConnected, wwwroot) == 0)
                {
                    ViewBag.ErrorCode = 2;
                    return View("ErrorPage");
                }
                else
                {
                    recipe.CreateDatabaseObject();

                }


                

                await _recipeRepository.CreateAsync(recipe);
                return View("CreationSuccess");
            }
            return View(recipe);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public new async Task<IActionResult> Edit(int id, IFormCollection files)
        {
            String wwwroot = @"C:\Users\Seba\Desktop\PSSC_Unpublished-master\MVC_CookBook_PSSC\MVC_CookBook_PSSC\wwwroot\";

            var originalRecipe = await _recipeRepository.GetRecipeAsync(id);
            
            Recipe recipe = originalRecipe;
            var creator = new User();
            Dictionary<String, StringValues> dictionary = new Dictionary<string, StringValues>()
            {
                ["strUsername"] = "Usernam",
                ["strPassword"] = "171562342182351161011532421932381442483812749244103521169811874362152619313262231127227",
                ["strName"] = "name",
                ["strSurename"] = "surename",
                ["strEmail"] = "email@email.email",
                ["strIBAN"] = "ibanuuu",
                ["strCNP"] = "1234567890123"

            };
            var form = new FormCollection(dictionary);
            await creator.CreateValueObjectUser(form);
            creator.CreateDatabaseObject();
            await recipe.CreateValueObjectRecipeAsync(files, creator, wwwroot);
            recipe.CreateDatabaseObject();



            if (id != recipe.strID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _recipeRepository.UpdateRecipeAsync(recipe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!base.RecipeExists(recipe.strID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }


    }
}
