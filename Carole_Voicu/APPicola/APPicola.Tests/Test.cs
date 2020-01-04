using System;
using System.Web.Mvc;
using Antlr.Runtime;
using APPicola.Controllers;
using APPicola.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Xania.AspNet.Simulator;
using Assert = NUnit.Framework.Assert;

namespace APPicola.Tests
{
    [TestClass]
    public class SignInTests
    {
        public double Title { get; private set; }

        [TestMethod]
        public void FirstTest()
        {
            var contr = new SignInController();
            var result = contr.Account(new SignIn { User = "admin", Password = "admin" });
            Assert.IsNotNull(result);
        }
    }
}
