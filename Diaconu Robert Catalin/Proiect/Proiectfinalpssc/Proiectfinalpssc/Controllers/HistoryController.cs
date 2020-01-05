using Proiectfinalpssc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiectfinalpssc.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult Show(CUSTOMER customerModel)
        {
            var x="s";
            var IBAN = customerModel.IBAN;


            using (PSSCEntities dbModel = new PSSCEntities())
            {
                foreach (TRANSACTION trn in dbModel.TRANSACTIONS)
                {
                    if (trn.USERIBAN == customerModel.IBAN)
                    {
                        //ViewBag.Message = trn;
                        return View(trn);
                    }
                    
                }


                return View("~/Views/History/Empty.cshtml");

            }

                
        }
    }
}