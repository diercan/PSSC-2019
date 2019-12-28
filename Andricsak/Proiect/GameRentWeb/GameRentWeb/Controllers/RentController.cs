using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GameRentWeb.GenericModels;
using GameRentWeb.Models;
using GameRentWeb.Repositories;
using GameRentWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameRentWeb.Controllers
{
    public class RentController : Controller
    {
        private readonly IDataBaseRepo<RentOrder> _rentOrders;
        private readonly IDataBaseRepo<Game> _games;
        private readonly IDataBaseRepo<User> _users;
        private readonly MessageBroker _broker;
        
        private int rentedGameid;
        public RentController(IDataBaseRepo<RentOrder> rentOrders, IDataBaseRepo<Game> games, MessageBroker broker,
            IDataBaseRepo<User> users)
        {
            _rentOrders = rentOrders;
            _games = games;
            _broker = broker;
            _users = users;
        }

        public virtual void SetTempData(string key,string message)
        {
            TempData[key] = message;
        }

        public virtual string GetSessionValue(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        public IActionResult ExtendView()
        {
            return View();
        }

        public IActionResult DisplayRents()
        {
            if (GetSessionValue("Username") == null)
            {
                SetTempData("Error","You need to login first!");
                return RedirectToAction("Index", "Game");
            }
            var loggedUser = _users.GetAllObjects().Result.FirstOrDefault(u => u.UserName.Equals(GetSessionValue("Username")));
            
            List<RentOrder> rents = _rentOrders.GetAllObjects().Result.Where(r => r.user.Id == loggedUser.Id).ToList();
            return View("MyRents", rents);
        }
        
        [HttpGet]
        public IActionResult Index(int id)
        {
            if(HttpContext.Session.GetString("Username") ==null)
            {
                SetTempData("Error", "You need to login first!");
                return RedirectToAction("Index","Game");
            }
            
            rentedGameid = id;
            var gameRented = _games.GetObjectById(rentedGameid).Result;
            ViewBag.GameName = gameRented.Name;
            if(gameRented.Quantity < 1)
            {
                SetTempData("Error","There are no copies available!");
                return RedirectToAction("Index", "Game");
            }
            return View();
        }

        public async Task<IActionResult> Rent(RentViewModel rentView)
        {
            // database operations
            var rentedGame = _games.GetAllObjects().Result.FirstOrDefault(g => g.Name == rentView.RentedGame);
            var user = _users.GetAllObjects().Result.FirstOrDefault(u => u.UserName.Equals(HttpContext.Session.GetString("Username")));
            var gameRented = _games.GetAllObjects().Result.FirstOrDefault(g => g.Name.Equals(rentView.RentedGame));
            
            rentedGame = rentedGame.RentGame();
            if(rentedGame == null)
            {
                SetTempData("Quantity", "No more products left on stock");
                return View("Index");
            }
            else
            {
                await _games.Update(gameRented);
            }
            var rent = rentView.Rent;
            user.RentOrders = new Collection<RentOrder>();
            // DDD operations (kind of)
            rent.GameRented = gameRented.Name;
            await rent.CreateRentAsync(_broker);
            user = user.AddRent(rent);

            if(user == null)
            {
                SetTempData("Funds", $"Not enough funds, payment is {rent.TotalPayment}$!");
                return View("Index");
            }
            else
            {
                await _rentOrders.Insert(rent);
                await _users.Update(user);
                HttpContext.Session.SetString("Balance", Convert.ToString(user.Balance));
                return RedirectToAction("Index", "Game");
            }
        }

        public async Task<IActionResult> Return(int id)
        {
            
            RentOrder selectedRent = _rentOrders.GetObjectById(id).Result;
            if(selectedRent.CurrentRentedDay == DateTime.Today)
            {
                SetTempData("Error","You can't return a game on the same day you rent it!");
                return RedirectToAction("DisplayRents", "Rent");
            }
            Game returnedGame =  _games.GetAllObjects().Result.Where(g => g.Name.Equals(selectedRent.GameRented)).FirstOrDefault();
            var user = _users.GetAllObjects().Result.FirstOrDefault(u => u.UserName.Equals(HttpContext.Session.GetString("Username")));

            // DDD operations (kind of)
            returnedGame = returnedGame.ReturnGame();
            selectedRent = await selectedRent.InterruptRentAsync(_broker);     
            user = user.RemoveRent(selectedRent);
            
            // database operations
            await _users.Update(user);
            await _rentOrders.Delete(selectedRent.Id);
            HttpContext.Session.SetString("Balance", user.Balance.ToString());

            return RedirectToAction("DisplayRents", "Rent");
        }

        [HttpGet]
        [Route("Rent/Extend/{id}/{days}")]
        public async Task<IActionResult> Extend(int id,int days)
        {
            
            var selectedRent =  _rentOrders.GetObjectById(id).Result;           
            selectedRent.RentPeriod += Convert.ToInt32(days);

            var selectedRentJson = JsonConvert.SerializeObject(selectedRent, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
            );

            await _broker.SendMessage(selectedRentJson, "RentToWorker");
            var rentReceived = _broker.ReceiveMessage("WorkerToRent").Result;

          

            var user = _users.GetAllObjects().Result.FirstOrDefault(u => u.UserName.Equals(HttpContext.Session.GetString("Username")));
            
            if(user.Balance < days * 3f)
            {
                SetTempData("Error","You don't have enough money to extend it's rent duartion");
                return RedirectToAction("DisplayRents", "Rent");
            }
            user.Balance -= days * 3f;
            HttpContext.Session.SetString("Balance", user.Balance.ToString());
            await _users.Update(user);
            await _rentOrders.Update(rentReceived);

            return RedirectToAction("DisplayRents", "Rent");
        }
    }
}