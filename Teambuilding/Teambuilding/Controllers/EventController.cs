using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teambuilding.Models;
using Teambuilding.Repository;
using Teambuilding.Services;

namespace Teambuilding.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventServices eventServices;
        
        public EventController(IEventServices eventServices)
        {
            this.eventServices = eventServices;
        }
        
        public ActionResult Index()
        {
            return View(eventServices.getAllEvents());
        }

        public ActionResult AddEvent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEvent(Event events)
        {
            try
            {
                events.id = Guid.NewGuid();
                eventServices.createEvent(events);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteEvent(Guid id)
        {
            Event events = eventServices.getEventById(id);
            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvent(Guid id, IFormCollection collection)
        {
            try
            {
                eventServices.deleteEvent(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}