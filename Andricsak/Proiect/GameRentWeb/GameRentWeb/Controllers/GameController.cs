using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameRentWeb.Models;
using GameRentWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GameRentWeb.Controllers
{
    public class GameController : Controller
    {
        private readonly IDataBaseRepo<Game> _games;
        public GameController(IDataBaseRepo<Game> games)
        {
            _games = games;
            
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

        public async Task<IActionResult> AddGame(Game game,IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                var something = game.CoverImage;
                byte[] p1 = null;
                using (var fs1 = Image.OpenReadStream())
                using (var ms1 = new MemoryStream())
                {
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();
                }
                game.CoverImage = p1;
                await _games.Insert(game);
            }
            return RedirectToAction("AddGameView");
        }
       
    }
}
