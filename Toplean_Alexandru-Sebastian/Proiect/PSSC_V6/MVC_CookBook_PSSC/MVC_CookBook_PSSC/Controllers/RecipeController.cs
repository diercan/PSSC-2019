using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevExpress.Web.Mvc.UI;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_CookBook_PSSC.Models;
using MVC_CookBook_PSSC.Models.UserComponents;
using MVC_CookBook_PSSC.Repositories;
using MySql.Data.MySqlClient;


/*
 * 
 * https://www.learnrazorpages.com/razor-pages/handler-methods
 * Use to fire buttons without reloading the page.(Used to like and dislike recipies on a page)
 * 
 * 
 * 
 * 
 * */
namespace MVC_CookBook_PSSC.Controllers
{
    public class RecipeController : Controller
    {
        
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;


        public RecipeController( IRecipeRepository recipeRepository, IUserRepository userRepository)
        {
    
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
     
        }

        public async Task<IActionResult> CreationSuccess()
        {
            var userConnected = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(u => u.strUsername == "Usernam");
            ViewBag.userConnected = userConnected;
            return View();
        }
        // GET: Recipe
        public async Task<IActionResult> Index()
        {
            var crtUser = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(m => m.strUsername == "Usernam");
            ViewBag.CrtUser = crtUser;
            return View(await _recipeRepository.GetRecipesToListAsync());
        }

        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var crtUser = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(m => m.strUsername == "Usernam");
            ViewBag.CrtUser = crtUser;

            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _recipeRepository.GetRecipeAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }
        public async Task<IActionResult> View(int? id)
        {
            var crtUser = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(m => m.strUsername == "Usernam");
            ViewBag.CrtUser = crtUser;

            if (id == null)
            {
                return NotFound();
            }



            var recipe = await _recipeRepository.GetRecipeAsync(id);
            var creator = await _userRepository.GetUserAsync(recipe.strUserID);
           
            recipe.SetUser(creator) ;
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipe/Create
        public async Task<IActionResult> Create()
        {
            var crtUser = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(m => m.strUsername == "Usernam");
            ViewBag.CrtUser = crtUser;

            return View();
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection files)
        {
            Recipe recipe = new Recipe();
            
            
            if (ModelState.IsValid)
            {

                String wwwroot = @"C:\Users\Seba\Desktop\PSSC_Unpublished-master\MVC_CookBook_PSSC\MVC_CookBook_PSSC\wwwroot\";
                var userConnected = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(u => u.strUsername == "Usernam");
                ViewBag.CrtUser = userConnected;
                string recipeName = files["strRecipeName"];

                var rec = await _recipeRepository.GetRecipesAsDbSet().FirstOrDefaultAsync(u => u.strRecipeName == recipeName && u.strCreatorUsername ==userConnected.strUsername);
                if (rec != null)
                {
                    ViewBag.ErrorCode = 1;
                    return View("ErrorPage");
                }
                

                if(await recipe.CreateValueObjectRecipeAsync(files,userConnected, wwwroot) == 0)
                {
                    ViewBag.ErrorCode = 2;
                    return View("ErrorPage");
                }
                else
                {
                    recipe.CreateDatabaseObject();

                }


                ViewBag.CrtUser = userConnected;
                
                await _recipeRepository.CreateAsync(recipe);

                var getRecipe =await _recipeRepository.GetRecipesAsDbSet().FirstOrDefaultAsync(m => m.strRecipeName == recipe.strRecipeName);
                ViewBag.rec = getRecipe;
                return View("CreationSuccess");
            }
            return View(recipe);
        }

        // GET: Recipe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var crtUser = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(m => m.strUsername == "Usernam");
            ViewBag.CrtUser = crtUser;

            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _recipeRepository.GetRecipeAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection files)
        {
            var crtUser = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(m => m.strUsername == "Usernam");
            ViewBag.CrtUser = crtUser;

            String wwwroot = @"C:\Users\Seba\Desktop\PSSC_Unpublished-master\MVC_CookBook_PSSC\MVC_CookBook_PSSC\wwwroot\";

            var originalRecipe = await _recipeRepository.GetRecipeAsync(id);
            var creator = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(u => u.strUsername == "Usernam");
            Recipe recipe = originalRecipe;

            await recipe.CreateValueObjectRecipeAsync(files,creator,wwwroot);
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
                    if (!RecipeExists(recipe.strID))
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

        // GET: Recipe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _recipeRepository.GetRecipeAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(id);

            String wwwroot = @"C:\Users\Seba\Desktop\PSSC_V4\MVC_CookBook_PSSC\MVC_CookBook_PSSC\wwwroot\";
            try
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(wwwroot + @"\images\" + recipe.strCreatorUsername  +@"\"+ recipe.strRecipeName);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                Directory.Delete(wwwroot + @"\images\" + recipe.strCreatorUsername + @"\" + recipe.strRecipeName);
                

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }
            await _recipeRepository.DeleteAsync(recipe);
            return RedirectToAction(nameof(Index));
        }

        protected bool RecipeExists(int id)
        {
            return _recipeRepository.RecipeExists(id);
        }

        [HttpPost]
        public IActionResult TriggerThumbsUp(int id, Recipe recipe) 
        {
            for(int i = 1; i < 3; i++)
            {

            }
            return View(recipe);
        }

        [HttpPost]
        public IActionResult TriggerThumbsDown(int id, Recipe recipe)
        {

            return View(recipe);
        }


    }
}
