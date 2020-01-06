using Sistem_gestiune_vanzari.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistem_gestiune_vanzari.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autorizare(Sistem_gestiune_vanzari.Models.LoginModel loginModel)
        {
            using (dbSistem_gestiune_vanzariEntities _DBEntitate = new dbSistem_gestiune_vanzariEntities())
            {
                var detalii_Login = _DBEntitate.Table_Login.Where(x => x.UserName == loginModel.UserName && x.Password == loginModel.Password).FirstOrDefault();
                if(detalii_Login == null)
                {
                    loginModel.LoginErrorMessage = "Wrong username or password";
                    return View("Login",loginModel);
                }
                else
                {
                    Session["userID"] = detalii_Login.UserID;
                    return RedirectToAction("Dashboard", "Admin");
                }
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login","Login");
        }
    }
}