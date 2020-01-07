using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using PSSC.Repository;
using PSSC.Models;
using PSSC.Models.Postare;
using PSSC.Controllers;
using PSSC.Services;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace PSSCTests
{
    public class LoginControllerShould
    {
        [Fact]
        public void ReturnIndexAfterSuccesfulLogin()
        {
            var repositoryMock = new Mock<IUserRepository>();
            string DummyString1 = "Dummy1", DummyString2 = "Dummy2";
            User DummyUser = new User()
            {
                Username = DummyString1,
                Password = DummyString2
            };
            

            repositoryMock.Setup(x => x.GetUser(DummyString1, DummyString2))
                    .Returns(DummyUser);

            var loginService = new LoginService(repositoryMock.Object);

            var controller = new LoginController(loginService);

            // Act
            var result = controller.Login(DummyUser);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<PostareViewModel>>(viewResult.ViewData.Model);

        }

        [Fact]
        public void ReturnIndexAfterFailLogin()
        {
            //Arrange
            var repository = new UserRepository();
            User DummyUser = null;

            var loginService = new LoginService(repository);

            var controller = new LoginController(loginService);

            // Act
            var result = controller.Login(DummyUser);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
        }
    }
}
