using Business.Api;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class VotingSessionController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SessionStats()
        {
            var latestSession = ApiConsumer<VotingSession>.ConsumeGet("VotingSessions", 0);
            VotingSessionViewModel latestSessionViewModel;
            if (latestSession != null)
            {
                latestSessionViewModel = new VotingSessionViewModel
                {
                    Name = latestSession.Name,
                    StartDate = latestSession.StartDate,
                    EndDate = latestSession.EndDate,
                    Candidates = (ICollection<CandidateViewModel>)latestSession.Candidates
                };
                if (latestSession.EndDate > DateTime.Now)
                    return PartialView("SessionStats", latestSessionViewModel);
            }
            latestSessionViewModel = new VotingSessionViewModel();
            return PartialView("Create", latestSessionViewModel);
        }

        [HttpPost]
        public ActionResult Create(VotingSessionViewModel votingSessionViewModel)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}