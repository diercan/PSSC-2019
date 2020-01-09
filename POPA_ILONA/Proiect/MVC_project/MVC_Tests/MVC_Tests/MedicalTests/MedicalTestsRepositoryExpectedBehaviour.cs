using MVC_project.Models;
using MVC_project.Repository;
using System;
using Xunit;

namespace MVC_Tests
{

    public class MedicalTestsRepositoryExpectedBehaviour
    {
        [Fact]
        public void GetAllMedicalTests()
        {

            // Arrange
            var repo = new MedicalTestsRepository();

            // Act
            var result = repo.GetAllMedicalTests();

            // Assert
            Assert.Equal(3, result.Count);
        }
        
        [Fact]
        public void CreateMedicalTests()
        {
            // Arrange
            var repo = new MedicalTestsRepository();
            var medicalTests = new MedicalTests()
            {
                Id = Guid.NewGuid(),
                Name = "Name Test",
                Phone = 0735549508,
                TestName = TestName.CompleteBloodCount,
                Date = DateTime.Now,
                TestedComponent = "Platelets",
                NormalValues = "150,000 to 450,000/mcL",
                ResultedValues = "350,000/mcL",
                Diagnosis = "Healthy",
            };

            // Act
            repo.CreateMedicalTests(medicalTests);
            var result = repo.GetAllMedicalTests();

            // Assert
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void DeleteMedicalTests()
        {
            // Arrange
            var repo = new MedicalTestsRepository();
            var medicalTests = new MedicalTests(); 

            // Act
            repo.DeleteMedicalTests(medicalTests);
            var result = repo.GetAllMedicalTests();

            // Assert
            Assert.Equal(3, result.Count);
        }
    }

}