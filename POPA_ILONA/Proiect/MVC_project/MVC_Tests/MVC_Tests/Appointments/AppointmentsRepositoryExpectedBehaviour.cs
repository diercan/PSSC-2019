using MVC_project.Models;
using MVC_project.Repository;
using System;
using Xunit;

namespace MVC_Tests
{

    public class AppointmentsRepositoryExpectedBehaviour
    {
        [Fact]
        public void GetAllAppointments()
        {

            // Arrange
            var repo = new AppointmentsRepository();

            // Act
            var result = repo.GetAllAppointments();

            // Assert
            Assert.Equal(3, result.Count);
        }
        
        [Fact]
        public void CreateAppointments()
        {
            // Arrange
            var repo = new AppointmentsRepository();
            var appointments = new Appointments()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "Name Test",
                Phone = 0773351279,
                TestType = TestType.ThyroidFunctionTests
            };

            // Act
            repo.CreateAppointments(appointments);
            var result = repo.GetAllAppointments();

            // Assert
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void DeleteAppointments()
        {
            // Arrange
            var repo = new AppointmentsRepository();
            var appointments = new Appointments(); 

            // Act
            repo.DeleteAppointments(appointments);
            var result = repo.GetAllAppointments();

            // Assert
            Assert.Equal(3, result.Count);
        }
    }

}