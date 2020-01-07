using APPicola.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace APPicola.Controllers
{
    public class HomeController : Controller
    {
        ConnImplement ci = new ConnImplement();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Articole()
        {
            ModelState.Clear();
            return View(ci.GetSqlRowsArticole());
        }
    }
}