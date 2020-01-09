using Microsoft.AspNetCore.Mvc;
using Moq;
using MVC_project.Controllers;
using MVC_project.Models;
using MVC_project.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MVC_Tests
{
    public class MedicalTestsControllerExpectedBehaviour
    {
        //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.0

        [Fact]
        public void ReturnIndex()
        {
            // Arrange
            var mockRepo = new Mock<IMedicalTestsRepository>();
            mockRepo.Setup(repo => repo.GetAllMedicalTests())
                .Returns(MedicalTestData.MedicalTests);

            var controller = new MedicalTestsController(mockRepo.Object);

            // Act
            var result = controller.List();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<MedicalTests>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());

            Assert.Equal("Andreescu Valentina", model[0].Name);
            Assert.Equal(TestName.CompleteBloodCount, model[0].TestName);
            Assert.Equal("Morariu Vasile", model[1].Name);
            Assert.Equal(TestName.ThyroidFunctionTests, model[1].TestName);

        }
    }
}
