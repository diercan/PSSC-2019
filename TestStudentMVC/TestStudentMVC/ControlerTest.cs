using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentMVC.Controllers;
using StudentMVC.Models;
using StudentMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestStudentMVC
{
    public class ControlerTest
    {
        [Fact]
        public void ReturnIndex()
        {
            // Arrange
            //var bus = new Mock<IBusControl>();
            var repo = new Mock<IFeedbackRepo>();

            repo.Setup(repo => repo.GetAllMyFeedbacks()).Returns(GetFeedback);

            //var service = new ReservationService(bus.Object, repo.Object);

            var controller = new FeedbackController(repo.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);



            var model = Assert.IsAssignableFrom<List<Feedback>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());

            //Assert.Equal("Tudor", model[0].Name);
            //Assert.Equal(WashMachineType.Gorenje, model[0].WashMachineType);
            //Assert.Equal("Tudor2", model[1].Name);
            //Assert.Equal(WashMachineType.Bosh, model[1].WashMachineType);

        }

        private List<Feedback> GetFeedback()
        {
            return new List<Feedback>()
            {
                new Feedback(){
                Id=new Guid(),

                BadFeedback= "123",
                GoodFeedback = "321",
                Profesor = ProfesorList.Albert_Moza
                

        },
                new Feedback(){

                    Id=new Guid(),

                BadFeedback= "789",
                GoodFeedback = "987",
                Profesor = ProfesorList.Dan_Brown

                }
            };
        }
    }
}
