using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MyPlanner.Models;
using MyPlanner.Repository;
using MyPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MyPlanner.Tests
{
    [TestFixture]
    public class UserControllerTest
    {
        [Test]
        [Category("pass")]
        public void IndexShouldWork()
        {

            //Arrange
            var repositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var list = new List<User>() { };
            repositoryMock
                    .Setup(m => m.AddItem(It.IsAny<User>()))
                    .Callback<User>(item => list.Add(item));
            repositoryMock.Setup(m => m.GetAllItems()).Returns(list);

            var usersController = new UsersController(null,repositoryMock.Object);
            usersController.use_test_repository = true;
            var userItem = new User("test_name", "test_username", "test_password");

            //act
            var temp = usersController.Create(userItem);
            var result = usersController.Index_test();
           
            //assert
            Assert.IsInstanceOf(typeof(ViewResult), result);            
            var viewResult = (ViewResult)result;
            Assert.AreEqual("Index", viewResult.ViewName);
            var model = viewResult.Model;
            Assert.IsInstanceOf(typeof(List<User>),model);
            var todoList = (List<User>)model;
            Assert.AreEqual(1, todoList.Count);
            Assert.AreEqual("test_name", todoList.First().name);
            
        }
        [Test]
        [Category("pass")]
        public void LoginNotRegistered()
        {
            //Arrange
            var repositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            var list = new List<User>() { };
            var userItem = new User("test_name", "test_username", "test_password");
            repositoryMock
                    .Setup(m => m.AddItem(It.IsAny<User>()))
                    .Callback<User>(item => list.Add(item));
            repositoryMock.Setup(m => m.GetAllItems()).Returns(list);
            
            var usersController = new UsersController(null, repositoryMock.Object);
            usersController.use_test_repository = true;
            
            //act
            var temp = usersController.Create(userItem);
            userItem.encrypted_password = SecurePasswordHasherHelper.Hash("bad_pass");
            var result = usersController.Login(userItem);
           
            //assert
            Assert.IsInstanceOf(typeof(ViewResult), result);            
            var viewResult = (ViewResult)result;
            Assert.AreEqual("Login", viewResult.ViewName);
            var model = viewResult.Model;
            Assert.IsInstanceOf(typeof(User),model);
            var objUser = (User)model;            
            Assert.AreEqual("test_username", objUser.username);
        }
    }
}
