using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Data;
using Data.Model;
using Datc.Data.Repository;

namespace WebRole1.Controllers.api
{
    public class VotersController : ApiController
    {
        private readonly DatcDbContext db = new RepositoryFactory().GetContext();

        // GET: api/Voters
        public IQueryable<Voter> GetVoters()
        {
            return db.Voters.Include(v => v.SecretQuestions);
        }

        // GET: api/Voters/5
        [ResponseType(typeof(Voter))]
        public IHttpActionResult GetVoter(int id)
        {
            Voter voter = db.Voters.Include(v => v.SecretQuestions).Where(v => v.Id == id).SingleOrDefault();
            if (voter == null)
            {
                voter = db.Voters.Include(v => v.SecretQuestions).Where(v => v.Cnp == id.ToString()).SingleOrDefault();
                if (voter == null)
                    return NotFound();
            }

            return Ok(voter);
        }

        // PUT: api/Voters
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVoter(Voter voter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(voter).State = EntityState.Modified;
            foreach (var question in voter.SecretQuestions)
                if (question.Id == 0)
                    db.Entry(question).State = EntityState.Added;
                else
                    db.Entry(question).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoterExists(voter.Id))
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

        // POST: api/Voters
        [ResponseType(typeof(Voter))]
        public IHttpActionResult PostVoter(Voter voter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Voters.Add(voter);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = voter.Id }, voter);
        }

        // DELETE: api/Voters/5
        [ResponseType(typeof(Voter))]
        public IHttpActionResult DeleteVoter(int id)
        {
            Voter voter = db.Voters.Find(id);
            if (voter == null)
            {
                return NotFound();
            }

            db.Voters.Remove(voter);
            db.SaveChanges();

            return Ok(voter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoterExists(int id)
        {
            return db.Voters.Count(e => e.Id == id) > 0;
        }
    }
}