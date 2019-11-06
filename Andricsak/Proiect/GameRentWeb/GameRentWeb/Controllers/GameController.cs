using System;
using System.Collections.Generic;
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
        private List<Game> games;
        private string testDescription = "Why does the executive knife grab the rough ballet? The worse token clashs with a molecule after a desirable treasure. The biblical freeway fudges throughout the mouth. The arm accepts on top of a hierarchy! A dog breaks the back defect. A setting alert grasps the reform around the doomed exhibit.";
        public GameController(IDataBaseRepo<Game> games)
        {
            _games = games;
            //games = new List<Game>()
            //{
            //    new Game{Id=1,Name="God of war",Category="Adventure",Description=testDescription,Platform="PC, PS4",CoverImage="/images/TestImages/God-of-War.jfif" },
            //    new Game{Id=2,Name="Spider man",Category="Sci-Fi",Description=testDescription,Platform="PC, PS4",CoverImage="/images/TestImages/Marvels-Spider-Man.jfif"},
            //    new Game{Id=3,Name="Outlast",Category="Horror",Description=testDescription,Platform="PC, PS4",CoverImage="/images/TestImages/outlast_2_ps4_cover_boxart__40574.1514317536.jpg" },
            //    new Game{Id=4,Name="Fifa",Category="Sport",Description=testDescription,Platform="PC, PS4" }
            //};
        }

        public IActionResult Index()
        {
            var model = _games.GetAllObjects();
            return View(model);
        }
        public IActionResult Rent()
        {
            var currentUser = HttpContext.Session.GetString("Username");
            if(currentUser == null)
            {
                TempData["Error"] = "You must login first.";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}