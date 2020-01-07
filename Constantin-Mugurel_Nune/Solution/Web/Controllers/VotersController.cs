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
    public class VotersController : Controller
    {
        // GET: Voters
        public ActionResult Index()
        {
            var voters = ApiConsumer<Voter>.ConsumeGet("Voters");
            var votersViewModelList = from voter in voters
                                      select new VoterViewModel
                                      {
                                          Id = voter.Id,
                                          FirstName = voter.FirstName,
                                          LastName = voter.LastName,
                                          Cnp = voter.Cnp,
                                          SecretQuestionCounter = voter.SecretQuestions.Count
                                      };
            return View(votersViewModelList);
        }

        // GET: Voters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Voters/Create
        [HttpPost]
        public ActionResult Create(VoterViewModel voterViewModel)
        {
            try
            {
                var voter = new Voter
                {
                    FirstName = voterViewModel.FirstName,
                    Id = voterViewModel.Id,
                    LastName = voterViewModel.LastName,
                    Cnp = voterViewModel.Cnp,
                    SecretQuestions = new List<SecretQuestion>()
                };
                var taskResult = ApiConsumer<Voter>.ConsumePost("Voters", voter);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Voters/Edit/5
        public ActionResult Edit(int id)
        {
            var voter = ApiConsumer<Voter>.ConsumeGet("Voters", id);
            var voterViewModel = new VoterViewModel
            {
                FirstName = voter.FirstName,
                Id = voter.Id,
                LastName = voter.LastName,
                Cnp = voter.Cnp
            };
            return View(voterViewModel);
        }

        // POST: Voters/Edit/5
        [HttpPost]
        public ActionResult Edit(VoterViewModel voterViewModel)
        {
            try
            {
                var voter = new Voter
                {
                    FirstName = voterViewModel.FirstName,
                    Id = voterViewModel.Id,
                    LastName = voterViewModel.LastName,
                    Cnp = voterViewModel.Cnp
                };
                ApiConsumer<Voter>.ConsumePut("Voters", voter);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Voters/Delete/5
        public ActionResult Delete(int id)
        {
            var voter = ApiConsumer<Voter>.ConsumeGet("Voters", id);
            var voterViewModel = new VoterViewModel
            {
                FirstName = voter.FirstName,
                Id = voter.Id,
                LastName = voter.LastName,
                Cnp = voter.Cnp
            };
            return View(voterViewModel);
        }

        // POST: Voters/Delete/5
        [HttpPost]
        public ActionResult Delete(VoterViewModel voterViewModel)
        {
            try
            {
                ApiConsumer<object>.ConsumeDelete("Voters", voterViewModel.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Candidates/Details/5
        public ActionResult Details(int id)
        {
            var voter = ApiConsumer<Voter>.ConsumeGet("Voters", id);
            var voterViewModel = new VoterViewModel
            {
                FirstName = voter.FirstName,
                Id = voter.Id,
                LastName = voter.LastName,
                Cnp = voter.Cnp
            };
            voterViewModel.SecretQuestions = from secretQuestion in voter.SecretQuestions
                                             select new SecretQuestionViewModel
                                             {
                                                 Id = secretQuestion.Id,
                                                 Question = secretQuestion.Question,
                                                 Answer = secretQuestion.Answer
                                             };
            return View(voterViewModel);
        }

        public ActionResult AddSecretQuestion(int id)
        {
            return View();
        }

        // POST: Voters/Create
        [HttpPost]
        public ActionResult AddSecretQuestion(SecretQuestionViewModel secretQuestionViewModel, int id)
        {
            try
            {
                var secretQuestion = new SecretQuestion
                {
                    Question = secretQuestionViewModel.Question,
                    Answer = secretQuestionViewModel.Answer
                };

                var voter = ApiConsumer<Voter>.ConsumeGet("Voters", id);
                voter.SecretQuestions.Add(secretQuestion);
                var result = ApiConsumer<Voter>.ConsumePut("Voters", voter);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteSecretQuestion(int id)
        {
            var secretQuestion = ApiConsumer<SecretQuestion>.ConsumeGet("SecretQuestions", id);
            var secretQuestionViewModel = new SecretQuestionViewModel
            {
                Id = secretQuestion.Id,
                Question = secretQuestion.Question,
                Answer = secretQuestion.Answer,
            };
            return View(secretQuestionViewModel);
        }

        // POST: Voters/Delete/5
        [HttpPost]
        public ActionResult DeleteSecretQuestion(SecretQuestionViewModel secretQuestionViewModel)
        {
            try
            {
                ApiConsumer<object>.ConsumeDelete("SecretQuestions", secretQuestionViewModel.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditSecretQuestion(int id)
        {
            var voter = ApiConsumer<SecretQuestion>.ConsumeGet("SecretQuestions", id);
            var secretQuestionViewModel = new SecretQuestionViewModel
            {
                Id = voter.Id,
                Question = voter.Question,
                Answer = voter.Answer,
            };
            return View(secretQuestionViewModel);
        }

        // POST: Voters/Edit/5
        [HttpPost]
        public ActionResult EditSecretQuestion(SecretQuestion secretQuestionViewModel)
        {
            try
            {
                var secretQuestion = new SecretQuestion
                {
                    Id = secretQuestionViewModel.Id,
                    Question = secretQuestionViewModel.Question,
                    Answer = secretQuestionViewModel.Answer
                };
                ApiConsumer<SecretQuestion>.ConsumePut("SecretQuestions", secretQuestion);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
