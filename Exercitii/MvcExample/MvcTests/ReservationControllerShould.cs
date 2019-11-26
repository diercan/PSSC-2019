using Microsoft.AspNetCore.Mvc;
using Moq;
using MvcExample.Controllers;
using MvcExample.Models;
using MvcExample.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MvcTests
{
	public class ReservationControllerShould
	{
		//https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.0

		[Fact]
		public void ReturnIndex()
		{
			// Arrange
			var mockRepo = new Mock<IReservationRepository>();
			mockRepo.Setup(repo => repo.GetAllReservations())
				.Returns(ReservationTestData.Reservations);

			var controller = new ReservationController(mockRepo.Object);

			// Act
			var result = controller.Index();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<List<Reservation>>(viewResult.ViewData.Model);

			Assert.Equal(2, model.Count());

			Assert.Equal("Tudor", model[0].Name);
			Assert.Equal(WashMachineType.Gorenje, model[0].WashMachineType);
			Assert.Equal("Tudor2", model[1].Name);
			Assert.Equal(WashMachineType.Bosh, model[1].WashMachineType);

		}
	}
}
