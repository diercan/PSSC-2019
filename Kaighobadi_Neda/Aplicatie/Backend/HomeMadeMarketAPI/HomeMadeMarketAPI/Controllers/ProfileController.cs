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

namespace HomeMadeMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserProfileService _profileService;

        public ProfileController(UserProfileService service)
        {
            _profileService = service;
        }

        // GET: api/UserLogins
        [HttpGet]
        public ActionResult<List<UserProfile>> Get() => _profileService.Get();

        // GET: api/UserLogins/5
        [HttpGet("{id:length(24)}", Name = "GetProfiles")]
        public ActionResult<UserProfile> Get(string id)
        {
            var profile = _profileService.Get(id);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }

        // PUT: api/UserLogins/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, UserProfile profileIn)
        {
            var profile = _profileService.Get(id);
            if (profile == null)
            {
                return NotFound();
            }
            _profileService.Update(id, profileIn);
            return NoContent();
        }

        // POST: api/UserLogins
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public ActionResult<UserProfile> Create(UserProfile profile)
        {
            _profileService.Create(profile);

            return CreatedAtRoute("GetProfiles", new { id = profile.Id.ToString() }, profile);
        }

        // DELETE: api/UserLogins/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var profile
                = _profileService.Get(id);
            if (profile == null)
            {
                return NotFound();
            }

            _profileService.Remove(profile.Id);

            return NoContent();
        }
    }
}
