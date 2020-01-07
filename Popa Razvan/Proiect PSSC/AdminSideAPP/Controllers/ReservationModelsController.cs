using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminSideAPP.Data;
using AdminSideAPP.Models;
using MassTransit;
using NotificationClass;

namespace AdminSideAPP.Controllers
{
    public class ReservationModelsController : Controller
    {
        private readonly ReservationContext _context;
        private readonly IBusControl busControl;
        public ReservationModelsController(ReservationContext context)
        {
            _context = context;

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("amqp://xvtmmbgz:1C2wLj9NWM_LhGwm1jvx_lofx79WxStQ@stingray.rmq.cloudamqp.com/xvtmmbgz"), h =>
                {
                    h.Username("xvtmmbgz");
                    h.Password("1C2wLj9NWM_LhGwm1jvx_lofx79WxStQ");
                });
            });

            bus.Start();
            busControl = bus;
        }

        // GET: ReservationModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.ToListAsync());
        }

        // GET: ReservationModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationModel = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationModel == null)
            {
                return NotFound();
            }

            return View(reservationModel);
        }

        // GET: ReservationModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReservationModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Date,Time,SeatNumber,Details")] ReservationModel reservationModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationModel);
        }

        // GET: ReservationModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationModel = await _context.Reservations.FindAsync(id);
            if (reservationModel == null)
            {
                return NotFound();
            }
            return View(reservationModel);
        }

        // POST: ReservationModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Date,Time,SeatNumber,Details")] ReservationModel reservationModel)
        {
            if (id != reservationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationModelExists(reservationModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservationModel);
        }

        // GET: ReservationModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationModel = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservationModel == null)
            {
                return NotFound();
            }

            return View(reservationModel);
        }

        // POST: ReservationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationModel = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservationModel);
            await _context.SaveChangesAsync();
            await busControl.Publish(new ReservationDeleted()
            {
                Id = reservationModel.Id,
                FirstName = reservationModel.FirstName,
                LastName = reservationModel.LastName,
                Date = reservationModel.Date,
                Time = reservationModel.Time,
                SeatNumber = reservationModel.SeatNumber,
                Details = reservationModel.Details
            });
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationModelExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            return View();
        }

    }

    
}
