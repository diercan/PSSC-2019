using BooksApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace BooksApp.Controllers
{
    public class AccountController : Controller
    {
        private BooksDBEntities _db = new BooksDBEntities();
        // GET: Home

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                var acc = (from m in _db.Accounts
                            where m.Email == account.Email
                            select m).FirstOrDefault();

                var val = (from m in _db.Accounts
                           orderby m.Id descending
                           select m).FirstOrDefault();
                var id = val.Id;

                if (acc == null)
                {
                    account.Id = ++id;
                    _db.Accounts.Add(account);
                    _db.SaveChanges();
                    sendRegisterEmail(account.Email);
                }
                else
                {
                    return RedirectToAction("Register");
                }

                   
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            if (ModelState.IsValid)
            {
                var acc = (from m in _db.Accounts
                           where m.Username.Equals(account.Username) && m.Password.Equals(account.Password)
                           select m).First();

                if (acc != null)
                {
                    Session["UserID"] = acc.Id.ToString();
                    Session["UserName"] = acc.Username.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login");
                }

            }

            return RedirectToAction("Login");
        }

        private void sendRegisterEmail(string email)
        {
            string subject = "Register Complete";
            string body = "Wellcome to our website";
            string to = email;
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("krisztina.kis122@gmail.com");
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = body;
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = true;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("krisztina.kis122@gmail.com", "Anitszirk12-");
            smtp.Send(mm);
        }
    }
}
