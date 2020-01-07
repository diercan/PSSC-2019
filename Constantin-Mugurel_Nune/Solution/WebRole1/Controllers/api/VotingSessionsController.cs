using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Data;
using Data.Model;

namespace WebRole1.Controllers.api
{
    public class VotingSessionsController : ApiController
    {
        private DatcDbContext db = new DatcDbContext();

        // GET: api/VotingSessions
        public IQueryable<VotingSession> GetVotingSessions()
        {
            return db.VotingSessions;
        }

        // GET: api/VotingSessions/5
        [ResponseType(typeof(VotingSession))]
        public IHttpActionResult GetVotingSession(int id)
        {
            VotingSession votingSession = db.VotingSessions.Include(s => s.Candidates).Where(s => s.Id == id).SingleOrDefault();
            if (votingSession == null)
            {
                var latestDate = db.VotingSessions.Max(s => s.EndDate);
                votingSession = db.VotingSessions.Include(s => s.Candidates).Where(s => s.EndDate == latestDate).SingleOrDefault();
                if (votingSession == null)
                    return NotFound();
            }
            return Ok(votingSession);
        }

        // PUT: api/VotingSessions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVotingSession(VotingSession votingSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(votingSession).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VotingSessionExists(votingSession.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VotingSessions
        [ResponseType(typeof(VotingSession))]
        public IHttpActionResult PostVotingSession(VotingSession votingSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VotingSessions.Add(votingSession);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = votingSession.Id }, votingSession);
        }

        // DELETE: api/VotingSessions/5
        [ResponseType(typeof(VotingSession))]
        public IHttpActionResult DeleteVotingSession(int id)
        {
            VotingSession votingSession = db.VotingSessions.Find(id);
            if (votingSession == null)
            {
                return NotFound();
            }

            db.VotingSessions.Remove(votingSession);
            db.SaveChanges();

            return Ok(votingSession);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VotingSessionExists(int id)
        {
            return db.VotingSessions.Count(e => e.Id == id) > 0;
        }
    }
}