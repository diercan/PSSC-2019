using System;
using Microsoft.AspNetCore.Mvc;
using MVC_project.Controllers;
using MVC_project.Models;
using Xunit;

namespace MVC_Tests
{
    public class LoginControllerExpectedBehaviour
    {
        
        [Fact]

        public void Login()
        {
            //Arrange
            var login = new Login { Username = "", Password = "" };
            var controller = new LoginController();

            //Act
            var result = controller.Login(login) as ViewResult;

            //Assert
            Assert.Equal("Error", result.ViewName);

        }

    }

      
}


  