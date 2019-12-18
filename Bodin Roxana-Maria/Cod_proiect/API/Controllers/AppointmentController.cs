using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading;
using API.Services;

 namespace API.Controllers
 {
    [Route("api/[controller]")]
     public class AppointmentController : ControllerBase
    {
        public AppointmentController(AppDb db)
        {
            Db = db;
        }
       
        // GET api/appointment
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new AppointmentQuery(Db);
            var result = await query.LatestPostsAsync();
            return new OkObjectResult(result);
        }

        // GET api/appointment/5
        [HttpGet("{MedName}")]
        public async Task<IActionResult> GetOne(string medName)
        {
            await Db.Connection.OpenAsync();
            var query = new AppointmentQuery(Db);
            var result = await query.FindAsyncString(medName);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST api/med
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AppointmentService body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT api/med/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]AppointmentService body)
        {
            await Db.Connection.OpenAsync();
            var query = new AppointmentQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.MedName = body.MedName;
            result.Date = body.Date;
            result.PacientName = body.PacientName;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/med/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new AppointmentQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/med
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new AppointmentQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }

        public AppDb Db { get; }
    }
 }