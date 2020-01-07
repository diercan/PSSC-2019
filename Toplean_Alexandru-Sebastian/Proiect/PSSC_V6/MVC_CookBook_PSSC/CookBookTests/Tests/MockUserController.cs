using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Moq;
using MVC_CookBook_PSSC.Repositories;
using Xunit;
using MVC_CookBook_PSSC.Services;
using MVC_CookBook_PSSC.Models;
using Microsoft.EntityFrameworkCore;
using MVC_CookBook_PSSC.Controllers;
using Microsoft.AspNetCore.Mvc;
using MVC_CookBook_PSSC.Models.UserComponents;
using System.Linq;
using CookBookTests.TestDoubles;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using CookBookTests.Code;
using CookBookTests.Dependencies;
using System.Data.Entity;

namespace CookBookTests
{
    public  class MockRecipeController
    {

        int maxRange = 200;
        [Fact]
        public void MockIndexCalls()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<User>();

            var controller = new UserController(mockRepo.Object, null, new MessageBroker());

            controller.Index();

            mockRepo.Verify(m => m.CreateAsync(mockUser.Object), Times.Never);
            mockRepo.Verify(m => m.DeleteAsync(mockUser.Object), Times.Never);
            mockRepo.Verify(m => m.GetUserAsync(1), Times.Never);
            mockRepo.Verify(m => m.GetUsersAsDbSet(), Times.Never);
            mockRepo.Verify(m => m.GetUsersToListAsync(), Times.Never);
            mockRepo.Verify(m => m.UpdateUserAsync(mockUser.Object), Times.Never);
            mockRepo.Verify(m => m.UserExists(1), Times.Never);
        }

        [Fact]
        public async void MockDetailsCalls()
        {

            var mockRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<User>();

            var controller = new UserController(mockRepo.Object, null, new MessageBroker());




            await controller.Details(1);

            mockRepo.Verify(m => m.CreateAsync(mockUser.Object), Times.Never);
            mockRepo.Verify(m => m.DeleteAsync(mockUser.Object), Times.Never);
            mockRepo.Verify(m => m.GetUserAsync(1), Times.Once);
            for (int i = 2; i < maxRange; i++)
                mockRepo.Verify(m => m.GetUserAsync(i), Times.Never);
            mockRepo.Verify(m => m.GetUsersAsDbSet(), Times.Never);
            mockRepo.Verify(m => m.GetUsersToListAsync(), Times.Never);
            mockRepo.Verify(m => m.UpdateUserAsync(mockUser.Object), Times.Never);
            mockRepo.Verify(m => m.UserExists(1), Times.Never);
        }

        [Fact]
        public void MockSignUpParameterlessCalls()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<User>();

            var controller = new UserController(mockRepo.Object, null, new MessageBroker());

            controller.Signup();

            mockRepo.Verify(m => m.CreateAsync(mockUser.Object), Times.Never);
            mockRepo.Verify(m => m.DeleteAsync(mockUser.Object), Times.Never);
            for (int i = 1; i < maxRange; i++)
                mockRepo.Verify(m => m.GetUserAsync(i), Times.Never);
            mockRepo.Verify(m => m.GetUsersAsDbSet(), Times.Never);
            mockRepo.Verify(m => m.GetUsersToListAsync(), Times.Never);
            mockRepo.Verify(m => m.UpdateUserAsync(mockUser.Object), Times.Never);
            for (int i = 1; i < maxRange; i++)
                mockRepo.Verify(m => m.UserExists(i), Times.Never);
        }
        [Fact]
        public void MockLoginParameterlessCalls()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<User>();

            var controller = new UserController(mockRepo.Object, null, new MessageBroker());

            controller.Login().GetAwaiter();

            mockRepo.Verify(m => m.CreateAsync(mockUser.Object), Times.Never);
            mockRepo.Verify(m => m.DeleteAsync(mockUser.Object), Times.Never);
            for (int i = 1; i < maxRange; i++)
                mockRepo.Verify(m => m.GetUserAsync(i), Times.Never);
            mockRepo.Verify(m => m.GetUsersAsDbSet(), Times.Never);
            mockRepo.Verify(m => m.GetUsersToListAsync(), Times.Never);
            mockRepo.Verify(m => m.UpdateUserAsync(mockUser.Object), Times.Never);
            for (int i = 1; i < maxRange; i++)
                mockRepo.Verify(m => m.UserExists(i), Times.Never);
        
            
        }

        [Fact]
        public void MockCreateParameterlessCalls()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<User>();

            var controller = new UserController(mockRepo.Object, null, new MessageBroker());

            controller.Create();

            mockRepo.Verify(m => m.CreateAsync(mockUser.Object), Times.Never);
            mockRepo.Verify(m => m.DeleteAsync(mockUser.Object), Times.Never);
            for (int i = 1; i < maxRange; i++)
                mockRepo.Verify(m => m.GetUserAsync(i), Times.Never);
            mockRepo.Verify(m => m.GetUsersAsDbSet(), Times.Never);
            mockRepo.Verify(m => m.GetUsersToListAsync(), Times.Never);
            mockRepo.Verify(m => m.UpdateUserAsync(mockUser.Object), Times.Never);
            for (int i = 1; i < maxRange; i++)
                mockRepo.Verify(m => m.UserExists(i), Times.Never);


        }

        [Fact]
        public void MockLoginWithParametersStubCookieCalls()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<User>();

            
            var controllerStub = new UserControllerStub(mockRepo.Object,null, new MessageBroker());
            controllerStub.Login(null, new UserLoginModel() { strPassword = "Passw0rd",strUsername = "Usernam"},"cookie").GetAwaiter();

            mockRepo.Verify(m => m.CreateAsync(mockUser.Object), Times.Never);
            mockRepo.Verify(m => m.DeleteAsync(mockUser.Object), Times.Never);
            for (int i = 1; i < maxRange; i++)
                mockRepo.Verify(m => m.GetUserAsync(i), Times.Never);
            mockRepo.Verify(m => m.GetUsersAsDbSet(), Times.AtLeastOnce);
            mockRepo.Verify(m => m.GetUsersToListAsync(), Times.Never);
            mockRepo.Verify(m => m.UpdateUserAsync(mockUser.Object), Times.Never);
            for (int i = 1; i < maxRange; i++)
                mockRepo.Verify(m => m.UserExists(i), Times.Never);


        }



        [Fact]
        public void MockCreateCalls()
        {
            var mockRepo = new Mock<IUserRepository>();
      
            
            var controllerStub = new UserControllerStub(mockRepo.Object, null, new MessageBroker());

            var user = new User() { strPassword = "Passw0rd", strUsername = "Usernam", strEmail = "email@email.email", strMoney = 0, strName = "name", strSurename = "surename", strPremium = false, strIsLogged = false, strIpAddress = ":::1",strCNP="1234567890123",strCookie="cookie",strIBAN="ibanuuu" }; 
            controllerStub.Create(user).GetAwaiter();

            mockRepo.Verify(m => m.CreateAsync(user), Times.Once);
            mockRepo.Verify(m => m.DeleteAsync(user), Times.Never);
            for (int i = 1; i < maxRange; i++)
                mockRepo.Verify(m => m.GetUserAsync(i), Times.Never);
            mockRepo.Verify(m => m.GetUsersAsDbSet(), Times.Never);
            mockRepo.Verify(m => m.GetUsersToListAsync(), Times.Never);
            mockRepo.Verify(m => m.UpdateUserAsync(user), Times.Never);
            for (int i = 1; i < maxRange; i++)
                mockRepo.Verify(m => m.UserExists(i), Times.Never);
        }
        [Fact]
        public void MockLoginWithParametersStubShouldReturnIndex()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<User>();


            var userModel = new UserLoginModel() { strPassword = "Passw0rd", strUsername = "Usernam" };

            var controllerStub = new UserControllerStub(mockRepo.Object, null, new MessageBroker());


            Assert.Equal(0, controllerStub.LoginReturnInt(null, null, "cookie",null, false));
            Assert.Equal(1, controllerStub.LoginReturnInt(null, userModel, "cookie",null,false));
            Assert.Equal(2, controllerStub.LoginReturnInt(null, userModel, "cookie", new User(), false));
            Assert.Equal(2, controllerStub.LoginReturnInt(null, userModel, "cookie", new User(), true));
        }

        [Fact]
        public void MockEditReturns()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<User>();


            var user = new User() {ID = 1, strPassword = "Passw0rd", strUsername = "Usernam", strEmail = "email@email.email", strMoney = 0, strName = "name", strSurename = "surename", strPremium = false, strIsLogged = false, strIpAddress = ":::1", strCNP = "1234567890123", strCookie = "cookie", strIBAN = "ibanuuu" };

            var controllerStub = new UserControllerStub(mockRepo.Object, null, new MessageBroker());


            Assert.Equal(0, controllerStub.EditReturnInt(2, user, true, false,false));
            Assert.Equal(0, controllerStub.EditReturnInt(1, user, true, true, false));
            Assert.Equal(2, controllerStub.EditReturnInt(1, user, true, true, true));
            Assert.Equal(1, controllerStub.EditReturnInt(1, user, true, false, true));
            Assert.Equal(3, controllerStub.EditReturnInt(1, user, false, true, true));
        }


        [Fact]
        public void MockCreateLogspy()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<User>();

            LogSpy log = new LogSpy();

            var user = new User() { ID = 1, strPassword = "Passw0rd", strUsername = "Usernam", strEmail = "email@email.email", strMoney = 0, strName = "name", strSurename = "surename", strPremium = false, strIsLogged = false, strIpAddress = ":::1", strCNP = "1234567890123", strCookie = "cookie", strIBAN = "ibanuuu" };

            var controllerSpy = new UserControllerForLogSpy(mockRepo.Object, null, new MessageBroker());

            Dictionary<String, StringValues> dictionary = new Dictionary<string, StringValues>()
            {
                ["strUsername"] = "Usernam",
                ["strPassword"] = "171562342182351161011532421932381442483812749244103521169811874362152619313262231127227",
                ["strName"] = "name",
                ["strSurename"] = "surename",
                ["strEmail"] = "email@email.email",
                ["strIBAN"] = "ibanuuu",
                ["strCNP"] = "1234567890123"

            };
            var form = new FormCollection(dictionary);

            controllerSpy.SetAuditLog(log);

            controllerSpy.Create(form).GetAwaiter();

            Assert.Single(log.GetActions());
            Assert.Equal("Create function called with user name = Usernam", log.GetActions()[0]);


        }

        [Fact]
        public void TestIFormCollectionReturn()
        {
            Dictionary<String, StringValues> dictionary = new Dictionary<string, StringValues>()
            {
                ["strUsername"] = "Usernam",
                ["strPassword"] = "171562342182351161011532421932381442483812749244103521169811874362152619313262231127227",
                ["strName"] = "name",
                ["strSurename"] = "surename",
                ["strEmail"] = "email@email.email",
                ["strIBAN"] = "ibanuuu",
                ["strCNP"] = "1234567890123"

            };
            var form = new FormCollection(dictionary);

            var fakeDict = new FormCollection(dictionary);
            var mockRepo = new Mock<IUserRepository>();
            var fakeUser = new User() { strPassword = "Passw0rd", strUsername = "Usernam", strEmail = "email@email.email", strMoney = 0, strName = "name", strSurename = "surename", strPremium = false, strIsLogged = false, strIpAddress = ":::1", strCNP = "1234567890123", strCookie = "cookie", strIBAN = "ibanuuu" };

            mockRepo.Setup(m => m.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(new User())
                .Verifiable();


           

        }




    }
}
