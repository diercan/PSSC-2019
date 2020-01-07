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
    public class SecretQuestionsController : ApiController
    {
        private DatcDbContext db = new DatcDbContext();

        // GET: api/SecretQuestions
        public IQueryable<SecretQuestion> GetSecretQuestions()
        {
            return db.SecretQuestions;
        }

        // GET: api/SecretQuestions/5
        [ResponseType(typeof(SecretQuestion))]
        public IHttpActionResult GetSecretQuestion(int id)
        {
            SecretQuestion secretQuestion = db.SecretQuestions.Find(id);
            if (secretQuestion == null)
            {
                return NotFound();
            }

            return Ok(secretQuestion);
        }

        // PUT: api/SecretQuestions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSecretQuestion(SecretQuestion secretQuestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(secretQuestion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecretQuestionExists(secretQuestion.Id))
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

        // POST: api/SecretQuestions
        [ResponseType(typeof(SecretQuestion))]
        public IHttpActionResult PostSecretQuestion(SecretQuestion secretQuestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SecretQuestions.Add(secretQuestion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = secretQuestion.Id }, secretQuestion);
        }

        // DELETE: api/SecretQuestions/5
        [ResponseType(typeof(SecretQuestion))]
        public IHttpActionResult DeleteSecretQuestion(int id)
        {
            SecretQuestion secretQuestion = db.SecretQuestions.Find(id);
            if (secretQuestion == null)
            {
                return NotFound();
            }

            db.SecretQuestions.Remove(secretQuestion);
            db.SaveChanges();

            return Ok(secretQuestion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SecretQuestionExists(int id)
        {
            return db.SecretQuestions.Count(e => e.Id == id) > 0;
        }
    }
}