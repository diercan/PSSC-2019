using GameRentWeb.Controllers;
using GameRentWeb.Models;
using GameRentWeb.Repositories;
using GenericWorker;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Http;
using TestProject.MockedClasses;
using System.Linq;
using RabbitMQ.Client;

namespace TestProject
{
    public class Tests
    {
        private Mock<IDataBaseRepo<User>> _users;
        private Mock<IDataBaseRepo<Game>> _games;
        private Mock<IDataBaseRepo<RentOrder>> _rents;
        Mock<HttpContext> _mockHttpContext;
        MockHttpSession _mockSession;

        private MessageBroker _broker;
        private List<User> _usersList;
        private List<Game> _gameList;
        private List<RentOrder> _rentList;
        private RentOperations _operations;
        // variable used for setting TempData
        [SetUp]
        public void Setup()
        {
            _usersList = new List<User>();
            _rentList = new List<RentOrder>();
            _broker = new MessageBroker(new ConnectionFactory() { Uri = new Uri("amqp://zswjrhxx:USPn7uoCvEEPxLVGO0XrzjhK9wDx3Gwq@reindeer.rmq.cloudamqp.com/zswjrhxx") }.CreateConnection());
            _users = new Mock<IDataBaseRepo<User>>();
            _mockHttpContext = new Mock<HttpContext>();
            _mockSession = new MockHttpSession();
            // mocking methods User
            _users.Setup(m => m.GetAllObjects()).ReturnsAsync(_usersList);
            _users.Setup(m => m.Insert(It.IsAny<User>()))
                        .Callback<User>(item => _usersList.Add(item));
            _users.Setup(m => m.GetObjectById(It.IsAny<int>()))
                        .Callback<int>(item => _usersList.Find(u => u.Id == item));
            _users.Setup(m => m.Update(It.IsAny<User>()))
                            .Callback<User>(u => {
                                var foundUser = _usersList.Find(item => item.Id == u.Id);
                                foundUser.RentOrders = u.RentOrders;
                                });

            // mocking methods Games
            _gameList = new List<Game>();
            _games = new Mock<IDataBaseRepo<Game>>();
            _games.Setup(m => m.GetAllObjects()).ReturnsAsync(_gameList);
            _games.Setup(m => m.Insert(It.IsAny<Game>()))
                        .Callback<Game>(item => _gameList.Add(item));

            //mocking methods - RentOrder
            _rents = new Mock<IDataBaseRepo<RentOrder>>();
            _rents.Setup(m => m.GetAllObjects()).ReturnsAsync(_rentList);
            _rents.Setup(m => m.Insert(It.IsAny<RentOrder>()))
                        .Callback<RentOrder>(item => _rentList.Add(item));
            _rents.Setup(m => m.GetObjectById(It.IsAny<int>()))
                        .Returns<int>(id => Task.FromResult(_rentList.SingleOrDefault(r => r.Id == id)))
                        .Callback<int>(item => _rentList.Find(r => r.Id == item));
            _rents.Setup(m => m.Delete(It.IsAny<int>()))
                        .Callback<int>( id => _rentList.RemoveAll(r => r.Id == id));
                        

            //mocking messageBroker
            
        }

        
        [Test]
        public void LoginViewVerify()
        {
            // Arrange
            var controller = new UserController(_users.Object);
            //Act
            var result = controller.LoginView();
            //Assert
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));                      
        }

        [Test]
        public async Task FailedLoginAsync()
        {
            //Arrange

            User testUser = new User { UserName = "Dorel", Password = "." };
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["LoginError"] = "Incorrect username or password!";
            var controller = new UserController(_users.Object)
            {
                TempData = tempData
            };

            //Act
            var result = await controller.Login(testUser);
            //Assert
            Assert.That(result, Is.TypeOf(typeof(RedirectToActionResult)));
            var viewResult = (RedirectToActionResult)result;
            Assert.AreEqual("LoginView", viewResult.ActionName);
            Assert.AreNotEqual("Index", viewResult.ActionName);
            
        }

        [Test]
        public async Task RegisterSucces()
        {
            //Arrange
            var controller = new UserController(_users.Object);
            User testUser = new User { UserName = "Daniel", Password = "Daniel2", Email = "Dorel@yahoo.com" ,Id=1};
            //Act

            var result = await controller.Register(testUser);

            //Assert
            Assert.That(result, Is.TypeOf(typeof(RedirectToActionResult)));
            var redirectViewResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectViewResult.ActionName);
            Assert.AreNotEqual("RegisterView", redirectViewResult.ActionName);

            Assert.AreEqual(1, _usersList.Count);
            Assert.AreEqual("Daniel", _usersList.Find(u => u.Id == testUser.Id).UserName);
        }


        [Test]
        public async Task AddGameSuccesAsync()
        {
            //arrange
            var environment = new Mock<IWebHostEnvironment>();
            var controller = new GameController(_games.Object,environment.Object);
            var testGame = new Game { Id = 1, Name = "Outlast", Quantity = 5, Category = "Horror", Platform = "PC" };
            // mock http context class and file upload method
            IFormFileCollection formCollection = new FormFileCollection();
            
            
            _mockHttpContext.Setup(s => s.Request.Form.Files).Returns(formCollection);
            controller.ControllerContext.HttpContext = _mockHttpContext.Object;
            //act
            var result = controller.AddGame(testGame);
            
            // asssert
            Assert.That(result, Is.TypeOf(typeof(Task<IActionResult>)));
            var resultView = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", resultView.ActionName);
            Assert.AreEqual(1, _gameList.Count); // verify that game has been added
            Assert.AreEqual("Outlast", _gameList.Find(u => u.Id == testGame.Id).Name);

        }

        [Test]
        public async Task DisplayRents()
        {
            //Arrange
            var controller = new RentController(_rents.Object, _games.Object, _broker, _users.Object);
            var testUser = new User { Id = 1, UserName = "Daniel", Balance = 120, RentOrders = new List<RentOrder>() };
            var testGame = new Game { Id = 1, Name = "God of War", Quantity = 4 };
            var testGame2 = new Game { Id = 1, Name = "Spider-man", Quantity = 6 };
            var testRent = new RentOrder
            {
                Id = 1,
                GameRented = "God of War",
                user = testUser,
                CurrentRentedDay = new DateTime(2019, 11, 20)
            ,
                ExpiringDate = new DateTime(2019, 11, 25),
                RentPeriod = 5
            };
            var testRent2 = new RentOrder
            {
                Id = 2,
                GameRented = "Spider-man",
                user = testUser,
                RentPeriod = 10,
            };

            _mockSession["Balance"] = testUser.Balance;
            _mockHttpContext.Setup(s => s.Session).Returns(_mockSession);

            controller.ControllerContext.HttpContext = _mockHttpContext.Object;
            testUser.RentOrders.Add(testRent2);
            testUser.RentOrders.Add(testRent);

            _rentList.Add(testRent);
            _rentList.Add(testRent2);
            _usersList.Add(testUser);
           

            //Act
            var result = controller.Rent(testRent);
            
            //Assert
            Assert.AreEqual(1, _usersList.Count);
            Assert.AreEqual(1, _rentList.Count);
            _rents.Verify(m => m.GetAllObjects(), Times.Never);
        }

        [Test]
        public void ReturnGame()
        {
            // arrange
            var controller = new RentController(_rents.Object, _games.Object, _broker, _users.Object);
            var testUser = new User { Id = 1, UserName = "Daniel", Balance = 100 ,RentOrders = new List<RentOrder>()};
            var testRent = new RentOrder { Id = 1, GameRented = "God of War", user = testUser,CurrentRentedDay=new DateTime(2019,11,20)
            , ExpiringDate=new DateTime(2019,11,25),RentPeriod=5};
            testUser.RentOrders.Add(testRent);
            var testGame = new Game { Id = 1, Name = "God of War", Quantity = 4 };
            _rentList.Add(testRent);
            _usersList.Add(testUser);
            _gameList.Add(testGame);
            _mockSession["Balance"] = testUser.Balance;
            _mockSession["Username"] = testUser.UserName;
            _mockHttpContext.Setup(s => s.Session).Returns(_mockSession);
            controller.ControllerContext.HttpContext = _mockHttpContext.Object;
            //act 
            var result = controller.Return(testRent.Id);
            Console.WriteLine(testUser.RentOrders.ToList().ToString());

            //assert
            var viewResult = (RedirectToActionResult)result.Result;
            Assert.AreEqual("DisplayRents", viewResult.ActionName);
            Assert.AreEqual(0, testUser.RentOrders.Count); // rent has been removed
            Assert.AreEqual(91, testUser.Balance);
            Assert.AreEqual(5, testGame.Quantity);
            
        }

        [TearDown]
        public void TearDown()
        {
            
        }
    }
}