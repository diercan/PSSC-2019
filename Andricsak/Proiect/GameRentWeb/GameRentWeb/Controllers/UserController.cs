using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRentWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using GameRentWeb.Models;
using Microsoft.AspNetCore.Http;

namespace GameRentWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IDataBaseRepo<User> _userRepository;
        
        
        public UserController(IDataBaseRepo<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult RegisterView()
        {
            return View();
        }
        public IActionResult LoginView()
        {
            return View();
        }
        
        public async Task<IActionResult> Register(User user)
        {
            if(ModelState.IsValid)
            {
                await _userRepository.Insert(user);
                return RedirectToAction("Index", "Game");
            }
            return RedirectToAction("RegisterView");
        }

        public async Task<IActionResult> Login(User user)
        {
            if(user.UserName != null && user.Password !=null)
            {
                User foundUser = _userRepository.GetAllObjects().Result.FirstOrDefault(e => e.UserName == user.UserName);
                if (foundUser != null && user.Password == foundUser.Password)
                {
                    HttpContext.Session.SetString("Username", user.UserName);
                    HttpContext.Session.SetString("Balance", Convert.ToString(foundUser.Balance));
                    return RedirectToAction("Index", "Game");

                }
                else
                {
                    TempData["LoginError"] = "Incorrect username or password!";
                }
            }
            return RedirectToAction("LoginView");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Balance");
            return RedirectToAction("Index", "Home");
        }
    }
}