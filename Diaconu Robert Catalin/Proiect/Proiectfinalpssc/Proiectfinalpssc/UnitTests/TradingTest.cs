using NUnit.Framework;
using Proiectfinalpssc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proiectfinalpssc.UnitTests
{

    public class TradingTest
    {
        [TestFixture]
        public class CustomerTest
        {
            CUSTOMER source;
            CUSTOMER destination;

            [SetUp]
            public void InitAccount()
            {
                
                source = new CUSTOMER();
                source.Deposit(300);
                destination = new CUSTOMER();
                destination.Deposit(100);
            }

            [Test]
            [Category("pass")]
            public void TransferFunds()
            {
               
                source.TransferFunds(destination, 200);
                Assert.AreEqual(200, destination.Balance);
                Assert.AreEqual(100, source.Balance);
            }

            [Test, Category("pass")]

            [TestCase(300, 0, 78)]
            [TestCase(200, 0, 90)]
            [TestCase(100, 0, 10)]
            public void TransferMinFunds(int a, int b, int c)
            {

                CUSTOMER source = new CUSTOMER();
                source.Deposit(a);
                CUSTOMER destination = new CUSTOMER();
                destination.Deposit(b);

                source.TransferMinFunds(destination, c);
                Assert.AreEqual(c, destination.Balance);
            }

            [Test]
            [Category("fail")]
            [TestCase(200, 150, 190)]
            [TestCase(200, 150, 325)]

            public void TransferMinFundsFail(int a, int b, int c)
            {
               CUSTOMER source = new CUSTOMER();
                source.Deposit(a);
                CUSTOMER destination = new CUSTOMER();
                destination.Deposit(b);

                destination = source.TransferMinFunds(destination, c);

            }
            [Test]
            [Category("fail")]
            [TestCase(200, 150, -1)]
            [TestCase(200, 150, -142)]
            [TestCase(100, 100, 20)]

            public void TransferNegativeAmount(int a, int b, int c)
            {
                CUSTOMER source = new CUSTOMER();
                source.Deposit(a);
                CUSTOMER destination = new CUSTOMER();
                destination.Deposit(b);

                destination = source.TransferMinFunds(destination, c);
            }


            [Test]
            [Category("fail")]
            [Combinatorial]

            public void TransferMinFundsFailAll([Values(200)] int a, [Values(0, 20)] int b, [Values(190, 345)]int c)
            {
                CUSTOMER source = new CUSTOMER();
                source.Deposit(a);
                CUSTOMER destination = new CUSTOMER();
                destination.Deposit(b);
                destination = source.TransferMinFunds(destination, c);
            }
            [Test]
            [Category("pass")]
            public void VerifyTransferFundsInSource()
            {
                source.TransferFunds(destination, 30);
                Assert.AreEqual(source.Balance, 270);
            }
            [Test]
            public void VerifyTransferFundsInDestination()
            {
                source.TransferFunds(destination, 30);
                Assert.AreEqual(destination.Balance, 130);
            }

        }
    }
}