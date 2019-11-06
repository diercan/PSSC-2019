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
        
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                _userRepository.Insert(user);
                return RedirectToAction("Game/Index");
            }
            return RedirectToAction("RegisterView");
        }

        public IActionResult Login(User user)
        {
            if(user.UserName != null && user.Password !=null)
            {
                User foundUser = _userRepository.GetObjectById(user.Id);
                if(foundUser != null)
                {
                    HttpContext.Session.SetString("Username", user.UserName);
                    return RedirectToAction("Game/Index");

                }
            }
            return RedirectToAction("LoginView");
        }
    }
}