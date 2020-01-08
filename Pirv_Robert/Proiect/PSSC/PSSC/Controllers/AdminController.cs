using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSSC.Models;
using PSSC.Repository;

namespace PSSC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRezervareRepository rezervareRepository;
        public AdminController(IRezervareRepository rezervareRepository)
        {
            this.rezervareRepository = rezervareRepository;
            
        }
        public ActionResult Index()
        {
            return View(rezervareRepository.ObtineRezervari());
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin, IFormCollection fc)
        {
            string var = fc["Password"];
            if (admin.username.Equals("admin") && admin.Password.Equals("1234"))
                return RedirectToAction(nameof(Index));
            else
                return RedirectToAction(nameof(ErrorViewModel));
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rezervare rezervare)
        {
            try
            {
                double pret = 0;
                rezervare.IdUnic = Guid.NewGuid();
                if (rezervare.murdarie.Equals(stareMasina.foarte_murdara))
                    pret = pret + 30;
                else if (rezervare.murdarie.Equals(stareMasina.murdara))
                    pret = pret + 20;
                else if (rezervare.murdarie.Equals(stareMasina.relativ_curata))
                    pret = pret + 15;

                if (rezervare.optiune1.Equals(optiuni.ceara) || rezervare.optiune2.Equals(optiuni.ceara) ||
                    rezervare.optiune3.Equals(optiuni.ceara) || rezervare.optiune4.Equals(optiuni.ceara))
                    pret = pret + 5;
                if (rezervare.optiune1.Equals(optiuni.portbagaj) || rezervare.optiune2.Equals(optiuni.portbagaj) ||
                    rezervare.optiune3.Equals(optiuni.portbagaj) || rezervare.optiune4.Equals(optiuni.portbagaj))
                    pret = pret + 10;
                if (rezervare.optiune1.Equals(optiuni.exterior) || rezervare.optiune2.Equals(optiuni.exterior) ||
                    rezervare.optiune3.Equals(optiuni.exterior) || rezervare.optiune4.Equals(optiuni.exterior))
                    pret = pret + 10;
                if (rezervare.optiune1.Equals(optiuni.interior) || rezervare.optiune2.Equals(optiuni.interior) ||
                    rezervare.optiune3.Equals(optiuni.interior) || rezervare.optiune4.Equals(optiuni.interior))
                    pret = pret + 10;
                rezervare.total = pret;
                rezervareRepository.CreareRezervare(rezervare);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Admin/Delete/5
        public ActionResult Delete(Guid id)
        {
            var rezervare = rezervareRepository.ObtineRezervareDupaGuid(id);
            return View(rezervare);
        }
        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var rezervare = rezervareRepository.ObtineRezervareDupaGuid(id);
                rezervareRepository.StergeRezervare(rezervare);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}