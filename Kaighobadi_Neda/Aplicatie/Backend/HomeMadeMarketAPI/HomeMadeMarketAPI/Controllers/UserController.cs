using System;
using System.Threading.Tasks;
using HomeMadeMarketAPI.Models;
using HomeMadeMarketAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeMadeMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly LoginService _loginService;
        public UserController(LoginService service)
        {
            _loginService = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _loginService.GetAll();
            return Ok(users);
        }
        [HttpGet("{id:length(24)}", Name = "GetUsers")]
        public ActionResult<User> Get(string id)
        {
            var user = _loginService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
