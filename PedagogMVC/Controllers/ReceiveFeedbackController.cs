using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using PedagogMVC.Repository;

namespace PedagogMVC.Controllers
{
    public class ReceiveFeedbackController : Controller
    {
        private IFeedbackRepo feedbackRepo;

        public ReceiveFeedbackController(IFeedbackRepo feedbackRepo)
        {
            this.feedbackRepo = feedbackRepo;
        }


        // GET: ReceiveFeedback
        public ActionResult Index()
        {
            return View(feedbackRepo.GetAllMyFeedbacks());
        }

        // GET: ReceiveFeedback/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReceiveFeedback/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReceiveFeedback/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReceiveFeedback feedback)
        {
            try
            {
                // TODO: Add insert logic here

                feedback.Id = Guid.NewGuid();
                feedbackRepo.CreateFeedback(feedback);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReceiveFeedback/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReceiveFeedback/Edit/5
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

        // GET: ReceiveFeedback/Delete/5
        public ActionResult Delete(Guid id)
        {
            var feedback = feedbackRepo.GetFeedbackById(id);
            return View();
        }

        // POST: ReceiveFeedback/Delete/5
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