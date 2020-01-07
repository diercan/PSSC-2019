using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMVC.Models;
using StudentMVC.Repository;
using StudentMVC.Services;

namespace StudentMVC.Controllers
{
    public class FeedbackController : Controller
    {

        private  IFeedbackRepo feedbackRepo;
        private readonly IService service;

        public FeedbackController(IFeedbackRepo feedbackRepo, IService service)
        {
            this.feedbackRepo = feedbackRepo;
            this.service = service;
        }


        // GET: Feedback
        public ActionResult Index()
        {
            

            return View(feedbackRepo.GetAllMyFeedbacks());
        }

        // GET: Feedback/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Feedback/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback)
        {
            try
            {
                feedback.Id = Guid.NewGuid();

                //  adaugă feedbackul creat în lista din memorie
                feedbackRepo.CreateFeedback(feedback);

                //  trimite feedback prin Publish-Subscribe RabbitMQ Queque
                service.SendFeedback(feedback);

               

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Feedback/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Feedback/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Feedback/Delete/5
        public ActionResult Delete(Guid id)
        {
            var feedback = feedbackRepo.GetFeedbackById(id);
            return View(feedback);
        }

        // POST: Feedback/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var feedback = feedbackRepo.GetFeedbackById(id);
                feedbackRepo.DeleteFeedback(feedback);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}