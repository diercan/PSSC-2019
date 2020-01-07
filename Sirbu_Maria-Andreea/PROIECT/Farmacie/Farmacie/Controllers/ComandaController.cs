using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Farmacie.Models;
using Farmacie.Models.Atributes;

namespace Farmacie.Controllers
{
    public class ComandaController : Controller
    {
        private readonly ComandaDbContext _context;

        public ComandaController(ComandaDbContext context)
        {
            _context = context;
        }

        // GET: Comanda
        public async Task<IActionResult> Index()
        {
            return View(await _context.comenzi.ToListAsync());
        }

        // GET: Comanda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.comenzi
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        // GET: Comanda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comanda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,strNumeFarmacist,strPrenumeFarmacist,strEmailFarmacist,strEmailDistribuitor,strNumeMedicament,inCantitate,strAdresaFarmacie")] Comanda comanda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comanda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comanda);
        }

        // GET: Comanda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.comenzi.FindAsync(id);
            if (comanda == null)
            {
                return NotFound();
            }
            return View(comanda);
        }

        // POST: Comanda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,strNumeFarmacist,strPrenumeFarmacist,strEmailFarmacist,strEmailDistribuitor,strNumeMedicament,inCantitate,strAdresaFarmacie")] Comanda comanda)
        {
            if (id != comanda.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comanda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComandaExists(comanda.ID))
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
            return View(comanda);
        }

        // GET: Comanda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.comenzi
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comanda == null)
            {
                return NotFound();
            }

            return View(comanda);
        }

        // POST: Comanda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comanda = await _context.comenzi.FindAsync(id);
            _context.comenzi.Remove(comanda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComandaExists(int id)
        {
            return _context.comenzi.Any(e => e.ID == id);
        }
    }
}
