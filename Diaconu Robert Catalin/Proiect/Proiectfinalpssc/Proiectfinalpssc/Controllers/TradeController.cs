using Proiectfinalpssc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Proiectfinalpssc.Controllers
{
    public class TradeController : Controller
    {
        // GET: Trade
        public ActionResult Index(CUSTOMER customerModel, string withdraw,string deposit, string transfer,string amount, string iban)
        {
           
            TRANSACTION transactionModel=new TRANSACTION();
            using (PSSCEntities dbModel = new PSSCEntities())
            {/*
                foreach (TRANSACTION trn in dbModel.TRANSACTIONS)
                    if (trn.USERIBAN == customerModel.IBAN)
                    {
                        transactionModel = trn;
                        */

                if (!string.IsNullOrEmpty(withdraw))//// make a withdrawal
                {
                 
                    if (Int32.Parse(customerModel.FIRSTNAME) < Int32.Parse(amount))
                    {
                      
                        ViewBag.Message = "Not enough funds to perform this action!";
                        return View();
                        //return RedirectToAction("Show", "History", customerModel);
                    }
                    else
                    {
                        customerModel.FIRSTNAME = (Int32.Parse(customerModel.FIRSTNAME) - Int32.Parse(amount)).ToString();
                        transactionModel.FIRSTNAME = amount;
                        Random rnd = new Random();
                        transactionModel.ID = rnd.Next(9999).ToString();
                        //transactionModel.USERIBAN = customerModel.IBAN;
                       // transactionModel.USERNAME = customerModel.USERNAME;
                        //transactionModel.LASTNAME = customerModel.LASTNAME;
                        //transactionModel.PASSWORD = "-";
                        //dbModel.TRANSACTIONS.Add(transactionModel);
                        //dbModel.SaveChanges();
                        ViewBag.Message = "asta e" + customerModel.IBAN;


                        //return RedirectToAction("Show", "History", customerModel);
                        return View();
                    }

                    
                }
                if (!string.IsNullOrEmpty(deposit))
                {




                    return RedirectToAction("Index", "Trade", customerModel);
                }
                if (!string.IsNullOrEmpty(transfer))
                {




                    return RedirectToAction("Show", "History", customerModel);
                }
            }
            return View();
           // return RedirectToAction("Show", "History", customerModel);


            /*public int Deposit(int amount)
            {
                FIRSTNAME = amount.ToString();
                return (Int32.Parse(CUSTOMER.FIRSTNAME) + amount);

            }

            public int Withdraw(int amount)
            {
                FIRSTNAME = amount.ToString();
                return (Int32.Parse(CUSTOMER.FIRSTNAME) - amount);
            }

            public void TransferFunds(CUSTOMER cst, int amount)
            {
                destination.Deposit(amount);
                Withdraw(amount);
            }
            */
        }
    }
}