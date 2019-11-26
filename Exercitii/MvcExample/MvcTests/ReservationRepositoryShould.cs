using MvcExample.Models;
using MvcExample.Repository;
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
			var repo = new ReservationRepository();

			// Act
			var result = repo.GetAllReservations();

			// Assert
			Assert.Equal(3, result.Count);
		}

		[Fact]
		public void CreateReservation()
		{
			// Arrange
			var repo = new ReservationRepository();
			var reservation = new Reservation()
			{
				Id = Guid.NewGuid(),
				Date = DateTime.Now,
				Name = "Test",
				WashMachineType = WashMachineType.Gorenje
			};

			// Act
			repo.CreateReservation(reservation);
			var result = repo.GetAllReservations();

			// Assert
			Assert.Equal(4, result.Count);
		}
	}
}
