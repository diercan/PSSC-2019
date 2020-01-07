using Proiectfinalpssc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiectfinalpssc.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Profile(CUSTOMER customerModel)
        {
            return View(customerModel);
        }
    }
}