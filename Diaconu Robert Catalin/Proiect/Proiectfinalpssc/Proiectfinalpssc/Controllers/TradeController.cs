using Proiectfinalpssc.Models;
using Proiectfinalpssc.Services.Mail_Service;
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
            {

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
                        transactionModel.ID = "4567";
                        transactionModel.USERIBAN =iban;
                        transactionModel.USERNAME = customerModel.USERNAME+"1";
                        transactionModel.LASTNAME = customerModel.LASTNAME;
                        transactionModel.PASSWORD = "-";
                        transactionModel.CUSTOMER = customerModel;
                        customerModel.TRANSACTIONS.Add(transactionModel);
                        dbModel.SaveChanges();
                        //dbModel.TRANSACTIONS.Add(transactionModel);
                        dbModel.SaveChanges();
                        Receive rcv = new Receive("Your account was deducted with the amount of " + amount+"dollars");
                        return RedirectToAction("Show", "History", customerModel);
                        //return View();
                    }

                    
                }
                if (!string.IsNullOrEmpty(deposit))
                {
                    customerModel.FIRSTNAME = (Int32.Parse(customerModel.FIRSTNAME) + Int32.Parse(amount)).ToString();
                    transactionModel.FIRSTNAME = amount;
                    Random rnd = new Random();
                    transactionModel.ID = rnd.Next(9999).ToString();
                    transactionModel.USERIBAN = iban;
                    transactionModel.USERNAME = customerModel.USERNAME + "1";
                    transactionModel.LASTNAME = customerModel.LASTNAME;
                    transactionModel.PASSWORD = "-";
                    transactionModel.CUSTOMER = customerModel;
                    customerModel.TRANSACTIONS.Add(transactionModel);
                    dbModel.SaveChanges();
                    //dbModel.TRANSACTIONS.Add(transactionModel);
                    dbModel.SaveChanges();
                    return RedirectToAction("Show", "History", customerModel);
                    //return View();
                }
                if (!string.IsNullOrEmpty(transfer))
                {
                    foreach (CUSTOMER cst in dbModel.CUSTOMERS)
                        if (cst.IBAN.CompareTo(iban) == 0)
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
                                cst.FIRSTNAME = (Int32.Parse(customerModel.FIRSTNAME) + Int32.Parse(amount)).ToString();
                                transactionModel.FIRSTNAME = amount;
                                Random rnd = new Random();
                                transactionModel.ID = rnd.Next(9999).ToString();
                                transactionModel.USERIBAN = iban;
                                transactionModel.USERNAME = customerModel.USERNAME + "1";
                                transactionModel.LASTNAME = customerModel.LASTNAME;
                                transactionModel.PASSWORD = "-";
                                transactionModel.CUSTOMER = customerModel;
                                customerModel.TRANSACTIONS.Add(transactionModel);
                                dbModel.SaveChanges();
                                //dbModel.TRANSACTIONS.Add(transactionModel);
                                dbModel.SaveChanges();
                                Receive rcv = new Receive("Transfer successful. Your account was deducted with the amount of " + amount + "dollars");
                                return RedirectToAction("Show", "History", customerModel);
                                //return View();
                            }
                        }
                            ViewBag.Message = "This iban doesn't exist in our database!";
                        
                    return View();
                }
            }
            return View();
          
        }
    }
}