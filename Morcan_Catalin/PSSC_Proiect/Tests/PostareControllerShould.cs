using Microsoft.AspNetCore.Mvc;
using Moq;
using PSSC.Controllers;
using PSSC.Models;
using PSSC.Repository;
using PSSC.Services;
using PSSC.Models.Postare;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using NUnit.Framework;
using MassTransit;

namespace PSSCTests
{
	public class PostareControllerShould
	{

		[Fact]
		public void ReturnIndex()
		{
            
			// Arrange
			var Repo = new PostareRepository();

            var mockService = new Mock<IPostareService>();

            mockService.Setup(mockService => mockService.GetAllPosts()).Returns(PostareTestData.Postari);



            var controller = new PostareController(mockService.Object);

			// Act
			var result = controller.Index();

			// Assert
			var viewResult = Xunit.Assert.IsType<ViewResult>(result);
			var model = Xunit.Assert.IsAssignableFrom<List<PostareViewModel>>(viewResult.ViewData.Model);

            Xunit.Assert.Equal(2, model.Count());

            Xunit.Assert.Equal("a", model[0].Autor);
            Xunit.Assert.Equal(GradImportantaPostare.Mediu, model[0].NivelImportanta);
            Xunit.Assert.Equal(TipPostare.Info, model[0].Tip);

            Xunit.Assert.Equal("b", model[1].Autor);
            Xunit.Assert.Equal(GradImportantaPostare.Mediu, model[1].NivelImportanta);
            Xunit.Assert.Equal(TipPostare.Info, model[1].Tip);

        }

        [Fact]
        public void CreeazaPostare()
        {
            // Arrange

            var mockService = new Mock<IPostareService>();

            var controller = new PostareController(mockService.Object);

            // Act
            var result = controller.CreeazaPostare();

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<PostareViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public void CreeazaPostarePost()
        {
            // Arrange
            var mockService = new Mock<IPostareService>();

            var controller = new PostareController(mockService.Object);

            var DummyPostareViewModel = new PostareViewModel();
            // Act
            var result = controller.CreeazaPostare(DummyPostareViewModel);

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<List<PostareViewModel>>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Stergere()
        {
            // Arrange
            var Repo = new PostareRepository();

            var mockService = new Mock<IPostareService>();

            mockService.Setup(mockService => mockService.GetAllPosts()).Returns(PostareTestData.Postari);



            var controller = new PostareController(mockService.Object);

            // Act
            var result = controller.Index();

        }


	}
}
