using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_CookBook_PSSC.Models;
using MVC_CookBook_PSSC.Models.CommonComponents;
using MVC_CookBook_PSSC.Models.EmailComponents;
using MVC_CookBook_PSSC.Models.Exceptions;
using MVC_CookBook_PSSC.Repositories;
using System.IO;
using RabbitMQ.Client;
using System.Text;
using MVC_CookBook_PSSC.Services;

namespace MVC_CookBook_PSSC.Controllers
{
    public class UserController : Controller
    {
      
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _accesor;
        private readonly MessageBroker _messageBroker;


        private User currentUser;

        public UserController(IUserRepository userRepository, IHttpContextAccessor accesor, MessageBroker messageBroker)
        {
            _userRepository = userRepository;

            _accesor = accesor;
            _messageBroker = messageBroker;
        }






        // GET: User
        public IActionResult Index()
        {
            var user = ViewBag.CrtUser;

            //ViewBag.Context = _userRepository.GetUsersAsDbSet();



            
            return View();


        }
        public IActionResult Signup()
        {
            return View();
        }


        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }


            var user = await _userRepository.GetUserAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }


        public async Task<IActionResult> Login()
        {

            String cookie = Request.Headers["Cookie"];

            if (cookie != null)
            {
                var userCookie = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(u => u.strCookie == cookie);
                if (userCookie == null)
                {
                    return View();
                }
                else
                {
                    ViewBag.CrtUser = userCookie;
                    return View("Index");
                }
            }
            else
            {
                return View();
            }



        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(IFormCollection form,[Bind("strUsername,strPassword")] UserLoginModel usr)
        {
            String cookie = Request.Headers["Cookie"];

            var userCookie = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(u => u.strCookie == cookie);
            if (userCookie == null)
            {
               
                string keepLogged = "false";
                try
                {
                    keepLogged = form["strIsLogged"].ToString();
                }
                catch
                {
                }
                string cryptedPassword = CryptoManager.ComputeSha256Hash(usr.strPassword);

                if (usr.strUsername == null)
                {
                    return NotFound();
                }

                var user = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(u => u.strUsername == usr.strUsername && u.strPassword == CryptoManager.ComputeSha256Hash(usr.strPassword));
                if (user == null)
                {
                    return View("Login");//no
                }
                else
                {

                    if (keepLogged == "true")
                    {

                        user.strCookie = cookie;
                        user.strIsLogged = true;

                        await _userRepository.UpdateUserAsync(user);
                    }
                    currentUser = user;
                    ViewBag.CrtUser = user;


                    return View("Index");//yes
                }
            }
            else
            {
                ViewBag.CrtUser = userCookie;
                return View("Index");
            }






        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            User user = new User();
            if (ModelState.IsValid)
            {

                await user.CreateValueObjectUser(form);
                user.CreateDatabaseObject();

                user.strIpAddress = Response.HttpContext.Connection.RemoteIpAddress.ToString();
                await _userRepository.CreateAsync(user);

                ViewBag.CrtUser = user;

                var encodedUserToSendMail = user.strEmail + "$" + user.strUsername;

                await _messageBroker.SendMailAsync(encodedUserToSendMail);
                return View("Index",user);

            }
            return View(user);

        }





        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var user =  await _userRepository.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection form)
        {
            User user = await _userRepository.GetUserAsync(id);
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await user.UpdateValueObjectUser(form);
                    user.CreateDatabaseObject();
                    await _userRepository.UpdateUserAsync(user);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userRepository.GetUserAsync(id);
            await _userRepository.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Logout()
        {


            var user = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(m=>m.strIsLogged == true);
            if (user == null)
            {
                return NotFound();
            }
            user.strIsLogged = false;
            await _userRepository.UpdateUserAsync(user);

            return View("Login");



        }

        private bool UserExists(int id)
        {
            return _userRepository.UserExists(id);
        }
    }
}
