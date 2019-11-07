using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRentWeb.Models;
using GameRentWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameRentWeb.Controllers
{
    public class RentController : Controller
    {
        private readonly IDataBaseRepo<RentOrder> _rentOrders;
        private readonly IDataBaseRepo<Game> _games;
        private readonly MessageBroker _broker;
        private static Game gameRented;
        private int rentedGameid;
        public RentController(IDataBaseRepo<RentOrder> rentOrders, IDataBaseRepo<Game> games,MessageBroker broker)
        {
            _rentOrders = rentOrders;
            _games = games;
            _broker = broker;
            
        }
        
        [HttpGet]
        public IActionResult Index(int id)
        {
            if(HttpContext.Session.GetString("Username") ==null)
            {
                TempData["Error"] = "You need to login First!";
                return RedirectToAction("Index","Game");
            }
            rentedGameid = id;
            gameRented = _games.GetObjectById(rentedGameid).Result;
            ViewBag.GameName = gameRented.Name;
            return View();
        }

        public async Task<IActionResult> Rent(RentOrder rent)
        {
            rent.CurrentRentedDay = DateTime.Today;
            rent.GameRented = gameRented.Name;

            var rentJson = JsonConvert.SerializeObject(rent);
            await _broker.SendMessage(rentJson);
            return RedirectToAction("Index","Game");
        }
    }
}