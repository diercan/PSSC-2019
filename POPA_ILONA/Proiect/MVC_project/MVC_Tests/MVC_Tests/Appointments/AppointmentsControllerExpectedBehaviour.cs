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
    public class AppointmentsControllerExpectedBehaviour
    {
        //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.0

        [Fact]
        public void ReturnIndex()
        {
            // Arrange
            var mockRepo = new Mock<IAppointmentsRepository>();
            mockRepo.Setup(repo => repo.GetAllAppointments())
                .Returns(AppointmentsTestData.Appointments);

            var controller = new AppointmentsController(mockRepo.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Appointments>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());

            Assert.Equal("Matei Alexandru", model[0].Name);
            Assert.Equal(TestType.BasicMetabolicPanel, model[0].TestType);
            Assert.Equal("Albulescu Adrian", model[1].Name);
            Assert.Equal(TestType.CompleteBloodCount, model[1].TestType);

        }
    }
}
