using APPicola.Models;
using System;
using System.Web.Mvc;


namespace APPicola.Controllers
{
    public class SignInController : Controller
    {
        ConnImplement ci = new ConnImplement();
        static SignIn SignIn_textbox = new SignIn();
        // GET: SignIn
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(SignIn model)
        {
            SignIn_textbox.User = model.User;
            SignIn_textbox.Password = model.Password;
            return View(SignIn_textbox);
        }
        [HttpPost]
        public ActionResult Account(SignIn acc)
        {
            ModelState.Clear();
            if (acc.User == "admin" && acc.Password == "admin")
            {
                ViewBag.Text = acc.User;
                return RedirectToAction("Index", "Admin");
            }
            else {
                for (int i = 0; i < ci.GetSqlRowsUsers().Count; i++)
                {
                    if (ci.GetSqlRowsUsers()[i].user == acc.User && ci.GetSqlRowsUsers()[i].password == acc.Password)
                    {
                            ViewBag.Text = acc.User;
                            return View("User");
                    }
                }
            }
            return View("SignIn");
        }
    }
}