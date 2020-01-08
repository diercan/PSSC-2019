using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teambuilding.Models;
using Teambuilding.Repository;
using Teambuilding.Controllers;

namespace Teambuilding.Controllers
{
    public class UserController : Controller
    {

    public UserController(ICredentialsRepository credentialsRepository)
        {
            this.credentialsRepository = credentialsRepository;
        }
    
        private readonly ICredentialsRepository credentialsRepository;
        
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Credentials user)
        {
            if(user.userName != null && user.password != null)
            {
                foreach(Credentials users in credentialsRepository.getCredentials())
                {
                    if(users.userName == user.userName && users.password == user.userName)
                    {
                        return RedirectToAction("Index","Home");
                    }
                }
            }
        else
        {
            TempData["LoginError"] = "Incorrect username or password";
        }
            return RedirectToAction("Index", "Home");
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}