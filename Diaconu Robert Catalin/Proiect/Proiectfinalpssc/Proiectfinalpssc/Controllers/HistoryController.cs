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
            //List<TRANSACTION> list = customerModel.TRANSACTIONS.ToList();
            //foreach(var trn in list)
            {
                //x = trn.USERIBAN;

            }
            // ViewBag.Message = list.Last();
            //ViewBag.Message = customerModel.TRANSACTIONS.Last();








            using (PSSCEntities dbModel = new PSSCEntities())
            {
                foreach (TRANSACTION trn in dbModel.TRANSACTIONS)
                    if (trn.USERIBAN == customerModel.IBAN)
                    {
                        //ViewBag.Message = trn;
                        return View(trn);
                    }
                return View();

            }

                
        }
    }
}