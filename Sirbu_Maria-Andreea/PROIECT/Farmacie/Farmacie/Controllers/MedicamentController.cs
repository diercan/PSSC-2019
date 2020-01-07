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
    public class MedicamentController : Controller
    {
        private readonly MedicamentDbContext _context;

        public MedicamentController(MedicamentDbContext context)
        {
            _context = context;
        }

        // GET: Medicament
        public async Task<IActionResult> Index()
        {
            return View(await _context.medicamente.ToListAsync());
        }

        // GET: Medicament/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicament = await _context.medicamente
                .FirstOrDefaultAsync(m => m.ID == id);
            if (medicament == null)
            {
                return NotFound();
            }

            return View(medicament);
        }

        // GET: Medicament/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicament/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,strNumeMedicament,inStoc,strDistribuitor,fPretVanzare,fPretCumparare,strLocatie")] Medicament medicament)
        {
            if (ModelState.IsValid)
            {
                Medicament m = new Medicament();
                m.CreateValueObject(medicament);
                m.CreateDBObject();

                _context.Add(m);
                _context.Add(medicament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicament);
        }

        // GET: Medicament/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicament = await _context.medicamente.FindAsync(id);
            if (medicament == null)
            {
                return NotFound();
            }
            return View(medicament);
        }

        // POST: Medicament/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,strNumeMedicament,inStoc,strDistribuitor,fPretVanzare,fPretCumparare,strLocatie")] Medicament medicament)
        {
            if (id != medicament.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentExists(medicament.ID))
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
            return View(medicament);
        }

        // GET: Medicament/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicament = await _context.medicamente
                .FirstOrDefaultAsync(m => m.ID == id);
            if (medicament == null)
            {
                return NotFound();
            }

            return View(medicament);
        }

        // POST: Medicament/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicament = await _context.medicamente.FindAsync(id);
            _context.medicamente.Remove(medicament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicamentExists(int id)
        {
            return _context.medicamente.Any(e => e.ID == id);
        }
        public async Task<IActionResult> Cautare(string searchString)
        {
            var farm = from m in _context.medicamente
                       select m;
            @ViewBag.flag = 0;
            if (!String.IsNullOrEmpty(searchString))
            {
                farm = farm.Where(s => s.strNumeMedicament.Contains(searchString));
                @ViewBag.flag = 1;
            }

            return View(await farm.ToListAsync());
        }
    }
}
