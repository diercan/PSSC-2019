using CookBookTests.Dependencies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_CookBook_PSSC.Controllers;
using MVC_CookBook_PSSC.Models;
using MVC_CookBook_PSSC.Repositories;
using MVC_CookBook_PSSC.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBookTests.Code
{
    public class UserControllerForLogSpy:UserController
    {

        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _accesor;
        private MessageBroker _broker;
        private ILogger _logger;
        public UserControllerForLogSpy(IUserRepository _userRepository, IHttpContextAccessor _accesor, MessageBroker _broker) :base(_userRepository,_accesor,_broker)
        {
            this._userRepository = _userRepository;
            this._accesor = _accesor;
            this._broker = _broker;
        }

        public void SetAuditLog(ILogger logger)
        {
            _logger = logger;
        }

        public new async Task<IActionResult> Create(IFormCollection form)
        {
            User user = new User();
            if (ModelState.IsValid)
            {

                await user.CreateValueObjectUser(form);
                user.CreateDatabaseObject();

                
                await _userRepository.CreateAsync(user);

                ViewBag.CrtUser = user;
                _logger.Log($"Create function called with user name = {form["strUsername"]}");


                var encodedUserToSendMail = user.strEmail + "$" + user.strUsername;

                //  await _messageBroker.SendMailAsync(encodedUserToSendMail);
                return View("Index", user);

            }
            return View(user);

        }
    }
}
