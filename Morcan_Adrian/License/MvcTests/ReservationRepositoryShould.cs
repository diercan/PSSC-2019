using MassTransit;
using Moq;
using LicenseWeb.Models;
using LicenseWeb.Repository;
using System;
using Xunit;

namespace MvcTests
{

	public class ReservationRepositoryShould
	{
		[Fact]
		public void GetAllReservations()
		{
			// Arrange
			var repo = new LicenseRepository();

			// Act
			var result = repo.GetAllReservations();

			// Assert
			Assert.Equal(3, result.Count);
		}

		[Fact]
		public void CreateReservation()
		{
			// Arrange
			var repo = new LicenseRepository();

			var reservation = new Licenses()
			{
				Id = Guid.NewGuid(),
				Name = "Test",
				Description = "TestDescription",
				Category = "TestCategory",
				Quantity = 1
			};
			// Act
			repo.CreateLicenses(reservation);
			var result = repo.GetAllReservations();

			// Assert
			Assert.Equal(4, result.Count);
		}
	}
}
