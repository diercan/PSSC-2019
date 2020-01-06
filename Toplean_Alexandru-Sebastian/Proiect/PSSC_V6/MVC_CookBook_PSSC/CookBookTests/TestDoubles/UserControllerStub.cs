using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CookBook_PSSC.Controllers;
using MVC_CookBook_PSSC.Models;
using MVC_CookBook_PSSC.Repositories;
using MVC_CookBook_PSSC.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBookTests.TestDoubles
{
    public class UserControllerStub : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _accesor;
        private readonly MessageBroker _messageBroker;
        public UserControllerStub(IUserRepository userRepository, IHttpContextAccessor accesor, MessageBroker messageBroker)
        {
            _userRepository = userRepository;

            _accesor = accesor;
            _messageBroker = messageBroker;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(IFormCollection form, [Bind("strUsername,strPassword")] UserLoginModel usr, String cookieForStub)
        {
            String cookie = cookieForStub;

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

        public int LoginReturnInt(IFormCollection form, [Bind("strUsername,strPassword")] UserLoginModel usr, String cookieForStub, User userObj, bool userRemembered)// userRemembered is used to replace the found user in the database that match the cookie with the database cookie
        {

            // 0 - not found
            // 1 - Login
            // 2 - Index
            String cookie = cookieForStub;

            bool userCookie = userRemembered;
            if (userCookie == false)
            {

                string keepLogged = "false";
                try
                {
                    keepLogged = form["strIsLogged"].ToString();
                }
                catch
                {
                }
                //string cryptedPassword = CryptoManager.ComputeSha256Hash(usr.strPassword);

                if (usr == null)
                {
                    return 0;
                }

                // var user = await _userRepository.GetUsersAsDbSet().FirstOrDefaultAsync(u => u.strUsername == usr.strUsername && u.strPassword == CryptoManager.ComputeSha256Hash(usr.strPassword));
                var user = userObj;
                if (user == null)
                {
                    return 1;//no
                }
                else
                {

                    if (keepLogged == "true")
                    {

                        user.strCookie = cookie;
                        user.strIsLogged = true;

                        //await _userRepository.UpdateUserAsync(user);
                    }

                    ViewBag.CrtUser = user;


                    return 2;//yes
                }
            }
            else
            {
                ViewBag.CrtUser = userCookie;
                return 2;
            }

        }

        public async Task<IActionResult> Create(User userStub)
        {
            await _userRepository.CreateAsync(userStub);
            
            return View("Index",userStub);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int EditReturnInt(int id, User usr, bool ModelState, bool RaiseException, bool UserExists)
        {
            // 0 - NotFound();
            // 1 - Edit Success;
            // 2 - catch - throw;
            // 3 - ModelState is Invalid

            User user = usr;
            if (id != user.ID)
            {
                return 0;
            }

            if (ModelState)
            {
                try
                {
                    if (RaiseException)
                    {
                        throw new DbUpdateConcurrencyException();
                    }
                    //await user.UpdateValueObjectUser(form);
                    user.CreateDatabaseObject();
                    //await _userRepository.UpdateUserAsync(user);
                    return 1;

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (UserExists == false)
                    {
                        return 0;
                    }
                    else
                    {
                        return 2;
                    }
                }
                
            }
            return 3;
        }

    }
}
