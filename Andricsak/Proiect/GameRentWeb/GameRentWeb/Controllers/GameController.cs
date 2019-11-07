using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GameRentWeb.Models;
using GameRentWeb.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GameRentWeb.Controllers
{
    public class GameController : Controller
    {
        private readonly IDataBaseRepo<Game> _games;
        private readonly IWebHostEnvironment _environment;
        public GameController(IDataBaseRepo<Game> games, IWebHostEnvironment environment)
        {
            _games = games;
            _environment = environment;
            
        }
        public async Task<IActionResult> Index()
        {
            var model = await _games.GetAllObjects();
            return View(model);
        }
        public IActionResult Rent()
        {
            var currentUser = HttpContext.Session.GetString("Username");
            if (currentUser == null)
            {
                TempData["Error"] = "You must login first.";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult AddGameView()
        {
            return View();
        }

        public async Task<IActionResult> AddGame(Game game)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {

                        var file = Image;
                        var uploads = Path.Combine(_environment.WebRootPath, "images\\uploads\\");

                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse
                                (file.ContentDisposition).FileName.Trim('"');

                            System.Console.WriteLine(fileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                game.CoverImage = file.FileName;
                            }


                        }
                    }
                }
                await _games.Insert(game);          
                return RedirectToAction("Index");

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View("Index");
        }
       
    }
}
