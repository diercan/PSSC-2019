using Business.Api;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class CandidatesController : Controller
    {
        // GET: Candidates
        public ActionResult Index()
        {
            var candidates = ApiConsumer<Candidate>.ConsumeGet("Candidates");
            var candidatesViewModel = from candidate in candidates
                    select new CandidateViewModel
                    {
                        Id = candidate.Id,
                        FirstName = candidate.FirstName,
                        LastName = candidate.LastName,
                        PartyName = candidate.PartyName
                    };
            return View(candidatesViewModel);
        }

        // GET: Candidates/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Candidates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Create
        [HttpPost]
        public ActionResult Create(CandidateViewModel candidateViewModel)
        {
            try
            {
                var candidate = new Candidate { FirstName = candidateViewModel.FirstName, 
                                                Id = candidateViewModel.Id, 
                                                LastName = candidateViewModel.LastName, 
                                                PartyName = candidateViewModel.PartyName };
                ApiConsumer<Candidate>.ConsumePost("Candidates", candidate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Candidates/Edit/5
        public ActionResult Edit(int id)
        {
            var candidate = ApiConsumer<Candidate>.ConsumeGet("Candidates", id);
            var candidateViewModel = new CandidateViewModel
            {
                FirstName = candidate.FirstName,
                Id = candidate.Id,
                LastName = candidate.LastName,
                PartyName = candidate.PartyName
            };
            return View(candidateViewModel);
        }

        // POST: Candidates/Edit/5
        [HttpPost]
        public ActionResult Edit(CandidateViewModel candidateViewModel)
        {
            try
            {
                var candidate = new Candidate
                {
                    FirstName = candidateViewModel.FirstName,
                    Id = candidateViewModel.Id,
                    LastName = candidateViewModel.LastName,
                    PartyName = candidateViewModel.PartyName
                };
                ApiConsumer<Candidate>.ConsumePut("Candidates", candidate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Candidates/Delete/5
        public ActionResult Delete(int id)
        {
            var candidate = ApiConsumer<Candidate>.ConsumeGet("Candidates", id);
            var candidateViewModel = new CandidateViewModel
            {
                FirstName = candidate.FirstName,
                Id = candidate.Id,
                LastName = candidate.LastName,
                PartyName = candidate.PartyName
            };
            return View(candidateViewModel);
        }

        // POST: Candidates/Delete/5
        [HttpPost]
        public ActionResult Delete(CandidateViewModel candidateViewModel)
        {
            try
            {
                ApiConsumer<object>.ConsumeDelete("Candidates", candidateViewModel.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}