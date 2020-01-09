using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;



namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        private IPreschool _preschool;

        public HomeController(IPreschool preschool)
        {
            _preschool = preschool;
        }
        public ActionResult Start()
        {
            return View();
        }

        public ActionResult Index()
        {
            var model = _preschool.GetAllPreschoolers();
            return base.View(model);
        }

        // GET: Preschool/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Preschool/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Preschool preschool)
        {
            try
            {
                    _preschool.CreatePreschool(preschool);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Details(Guid id)
        {
            var preschool = _preschool.GetPreschoolById(id);
            return View(preschool);
        }

        public ActionResult Delete(Guid id)
         {
            var preschool = _preschool.GetPreschoolById(id);
            return View(preschool);
         }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Preschool preschool)
		{
			try
			{
				_preschool.DeletePreschool(preschool);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

             
             

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
