using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeMadeMarketAPI.Models.UserLogin;
using HomeMadeMarketAPI.Services;
using HomeMadeMarketAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace HomeMadeMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginsController : ControllerBase
    {
        private readonly LoginService _loginService;
       // private IMapper _mapper;
        

        public UserLoginsController(LoginService service)
        {
            _loginService = service;
           
        }

      
        // POST: api/UserLogins
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            Console.Out.Write(login.Username);

            var user = await _loginService.Authenticate(login.Username, login.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect " });

            }

            return Ok(user);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _loginService.GetAll();
            return Ok(users);
        }

        [HttpPost("register")]
        public ActionResult<User> Create(User user)
        {
            System.Diagnostics.Debug.WriteLine(user);
            _loginService.Create(user);

            return CreatedAtRoute( new { id = user.Id.ToString() }, user);
        }

    }
}
