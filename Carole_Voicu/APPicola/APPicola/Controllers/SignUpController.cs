using APPicola.Models;
using System.Web.Mvc;


namespace APPicola.Controllers
{
    public class SignUpController : Controller
    {
        ConnImplement ci = new ConnImplement();
        static SignUp SignUp_textbox = new SignUp();
        // GET: SignUp
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(SignUp model)
        {
            SignUp_textbox.User = model.User;
            SignUp_textbox.Password = model.Password;
            SignUp_textbox.RePassword = model.RePassword;
            return View(SignUp_textbox);
        }
        [HttpPost]
        public ActionResult Account(SignUp acc)
        {
            ModelState.Clear();
            if (acc.Password == acc.RePassword)
            {
                ViewBag.Text = ci.InsertSqlRow(acc.User, acc.Password);
            }
            else
                return View("SignUp");
            return View("AddReport");
        }
    }
}