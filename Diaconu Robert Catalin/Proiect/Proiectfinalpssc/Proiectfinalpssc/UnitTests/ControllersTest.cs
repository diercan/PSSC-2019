using NUnit.Framework;
using Proiectfinalpssc.Controllers;
using Proiectfinalpssc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiectfinalpssc.UnitTests
{
    [TestFixture]
    public class ControllersTest
    {
        [Test]
        [Category("pass")]
        public void Login()
        {
            CUSTOMER customerModel=new CUSTOMER();
          /* customerModel.USERNAME = "cata5";
            customerModel.PASSWORD = "cata5";
             customerModel.FIRSTNAME = "hjk";
            customerModel.IBAN = "asd";
            customerModel.LASTNAME = "asdf";
            */
            LogInController controller = new LogInController();

            ViewResult result = controller.Login(customerModel) as ViewResult;
            Assert.AreEqual("LOGGED IN", result.ViewBag.LoggedMessage);
        }
    }
}