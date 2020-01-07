using Moq;
using MVC_CookBook_PSSC.Models;
using MVC_CookBook_PSSC.Models.EmailComponents;
using MVC_CookBook_PSSC.Models.Exceptions;
using MVC_CookBook_PSSC.Models.UserComponents;
using MVC_CookBook_PSSC.Repositories;
using NUnit.Framework;
using System.Collections.Generic;

namespace CookBookTests.Tests
{
    public class BasicTests
    {
        [Test]
        public void TestUserRepoSetups()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(m => m.GetUsersToListAsync()).ReturnsAsync(new List<User>());
            mockRepo.Setup(m => m.UserExists(1)).Returns(true);
            mockRepo.Setup(m => m.UserExists(2)).Returns(false);
            mockRepo.Setup(m => m.GetUserAsync(1)).ReturnsAsync(new User());
            mockRepo.Setup(m => m.GetUsersToListAsync()).Verifiable();

        }

        [Test]
        public void CheckAccountBind()
        {
            User user = new User();
            BankAccount account = new BankAccount(new IBAN(), 500);
            user.AddBankAccount(account);

            Assert.IsNotNull(user.GetBankAccount);
        }

        [Test]
        public void TestUserExceptions()
        {
            //Arrange
            User source = new User();
            User destination = new User();
            User userWithNullAccount = new User();
            EmailAddress email;


            BankAccount accountSrc = new BankAccount(new IBAN(), 500);
            BankAccount accountDst = new BankAccount(new IBAN(), 500);
            BankAccount nullAccount = null;

            //Act
            source.AddBankAccount(accountSrc);
            destination.AddBankAccount(accountDst);
            userWithNullAccount.AddBankAccount(nullAccount);
            email = new EmailAddress(new MVC_CookBook_PSSC.Models.CommonComponents.Text("ThrowMe"));

            //Assert
            Assert.Throws<InexistentBankAccountException>(() => source.TransferInBankAccount(nullAccount, 100));
            Assert.Throws<InexistentBankAccountException>(() => userWithNullAccount.TransferInBankAccount(accountDst, 100));
            Assert.Throws<InsufficientFundsException>(() => source.TransferInBankAccount(accountDst, 700));
            Assert.Throws<InvalidEmailException>(() => email.checkIfThrow());

        }

        [Test]
        public void TestTransferNoException()
        {
            User source = new User();
            User destination = new User();

            BankAccount accountSrc = new BankAccount(new IBAN(), 500);
            BankAccount accountDst = new BankAccount(new IBAN(), 500);

            source.AddBankAccount(accountSrc);
            destination.AddBankAccount(accountDst);


            source.TransferInBankAccount(accountDst, 200);
            Assert.AreEqual(accountDst.GetAmount, 700);
            Assert.AreEqual(accountSrc.GetAmount, 300);
        }


        [Test]
        public void TestRecipeMethods()
        {
            User user = null;
            var recipe = new Recipe();

            Assert.AreEqual(recipe.CreateValueObjectRecipeAsync(null, user, null).GetAwaiter().GetResult(), 0);

        }

        [Test]
        public void TestDepositInBankAccount()
        {
            User source = new User();
        
            BankAccount accountSrc = new BankAccount(new IBAN(), 500);

            source.AddBankAccount(accountSrc);
            source.DepositInBankAccount(300F);

            Assert.AreEqual(accountSrc.GetAmount, 800);
        }
    }
}