using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRentWeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using GameRentWeb.Models;
namespace GameRentWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IDataBaseRepo<User> _userRepository;
        public UserController(IDataBaseRepo<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}