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
    public class DistribuitorsController : Controller
    {
        private readonly DistribuitorDbContext _context;

        public DistribuitorsController(DistribuitorDbContext context)
        {
            _context = context;
        }

        // GET: Distribuitors
        public async Task<IActionResult> Index()
        {
            return View(await _context.distribuitori.ToListAsync());
        }

        // GET: Distribuitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distribuitor = await _context.distribuitori
                .FirstOrDefaultAsync(m => m.ID == id);
            if (distribuitor == null)
            {
                return NotFound();
            }

            return View(distribuitor);
        }

        // GET: Distribuitors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Distribuitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,strNumeDistribuitor,strEmailDistribuitor,strNrTelefonDistribuitor,strAdresaDistribuitor")] Distribuitor distribuitor)
        {
            if (ModelState.IsValid)
            {
                Distribuitor d = new Distribuitor();
                d.CreateValueObject(distribuitor);
                d.CreateDBObject();

                _context.Add(d);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(distribuitor);
        }

        // GET: Distribuitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distribuitor = await _context.distribuitori.FindAsync(id);
            if (distribuitor == null)
            {
                return NotFound();
            }
            return View(distribuitor);
        }

        // POST: Distribuitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,strNumeSistribuitor,strEmailDistribuitor,strNrTelefonDistribuitor,strAdresaDistribuitor")] Distribuitor distribuitor)
        {
            if (id != distribuitor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distribuitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistribuitorExists(distribuitor.ID))
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
            return View(distribuitor);
        }

        // GET: Distribuitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distribuitor = await _context.distribuitori
                .FirstOrDefaultAsync(m => m.ID == id);
            if (distribuitor == null)
            {
                return NotFound();
            }

            return View(distribuitor);
        }

        // POST: Distribuitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var distribuitor = await _context.distribuitori.FindAsync(id);
            _context.distribuitori.Remove(distribuitor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistribuitorExists(int id)
        {
            return _context.distribuitori.Any(e => e.ID == id);
        }
        public async Task<IActionResult> Cautare(string searchString)
        {
            var farm = from m in _context.distribuitori
                       select m;
            @ViewBag.flag = 0;
            if (!String.IsNullOrEmpty(searchString))
            {
                farm = farm.Where(s => s.strNumeDistribuitor.Contains(searchString));
                @ViewBag.flag = 1;
            }

            return View(await farm.ToListAsync());
        }
    }
}
