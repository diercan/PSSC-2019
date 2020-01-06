using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Moq;
using MVC_CookBook_PSSC.Repositories;
using Xunit;
using MVC_CookBook_PSSC.Services;
using MVC_CookBook_PSSC.Models;
using Microsoft.EntityFrameworkCore;
using MVC_CookBook_PSSC.Controllers;
using Microsoft.AspNetCore.Mvc;
using MVC_CookBook_PSSC.Models.UserComponents;
using System.Linq;
using CookBookTests.TestDoubles;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using CookBookTests.Code;
using CookBookTests.Dependencies;
using System.Data.Entity;
namespace CookBookTests.Tests
{
    public class MockReciepController
    {

        
        [Fact]
        public async Task MockIndexCalls()
        {
            var mockRepo = new Mock<IRecipeRepository>();
            var mockUserRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<Recipe>();

            var controller = new RecipeControllerStub(mockRepo.Object, mockUserRepo.Object);

            mockRepo.Setup(m => m.GetRecipesToListAsync()).ReturnsAsync(new List<Recipe>());
            await controller.Index();

            Recipe recipe = new Recipe()
            {
                strRecipeName = "Reteta",
                strCreatorUsername = "Usernam",
                strPreparation = "O faci",
                strRawIngredients = "raw shit",
                strComboBoxTag1 = "Vegan",
                strComboBoxTag2 = "Vegan",
                strComboBoxTag3 = "Vegan",
                strID = 1,
                strIngredients = "Shit",
                strUserID = 1,
                strPrice = 500F
            };


            mockRepo.Setup(m => m.CreateAsync(recipe)).Verifiable();

            mockRepo.Verify(m => m.CreateAsync(It.IsAny<Recipe>()), Times.Never);
            mockRepo.Verify(m => m.DeleteAsync(It.IsAny<Recipe>()), Times.Never);
            mockRepo.Verify(m => m.GetRecipeAsync(It.IsAny<int>()), Times.Never);
            mockRepo.Verify(m => m.GetRecipesAsDbSet(), Times.Never);
            mockRepo.Verify(m => m.GetRecipesToListAsync(), Times.Once);
            mockRepo.Verify(m => m.UpdateRecipeAsync(It.IsAny<Recipe>()), Times.Never);
            mockRepo.Verify(m => m.RecipeExists(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task MockViewCalls()
        {
            var mockRepo = new Mock<IRecipeRepository>();
            var mockUserRepo = new Mock<IUserRepository>();
            var mockUser = new Mock<Recipe>();

            //var controller = new RecipeControllerStub(mockRepo.Object, mockUserRepo.Object);
            var controller = new RecipeControllerStub(mockRepo.Object, mockUserRepo.Object);

            await controller.View(It.IsAny<int>());

            mockRepo.Verify(m => m.CreateAsync(It.IsAny<Recipe>()), Times.Never);
            mockRepo.Verify(m => m.DeleteAsync(It.IsAny<Recipe>()), Times.Never);
            mockRepo.Verify(m => m.GetRecipeAsync(It.IsAny<int>()), Times.Once);
            mockRepo.Verify(m => m.GetRecipesAsDbSet(), Times.Never);
            mockRepo.Verify(m => m.GetRecipesToListAsync(), Times.Never);
            mockRepo.Verify(m => m.UpdateRecipeAsync(It.IsAny<Recipe>()), Times.Never);
            mockRepo.Verify(m => m.RecipeExists(It.IsAny<int>()), Times.Never);

            
        }
        [Fact]
        public async Task MockCreateCalls()
        {
            var mockRepo = new Mock<IRecipeRepository>();
            var mockUserRepo = new Mock<IUserRepository>();

            //var controller = new RecipeControllerStub(mockRepo.Object, mockUserRepo.Object);
            var controller = new RecipeControllerStub(mockRepo.Object, mockUserRepo.Object);

            mockRepo.Setup(m => m.CreateAsync(It.IsAny<Recipe>())).Returns(Task.CompletedTask);

            Dictionary<String, StringValues> dictionary = new Dictionary<string, StringValues>()
            {
                ["strRecipeName"] = "Sarmaleee",
                ["strRawIngredients"] = "ceva 100 a",
                ["strPreparation"] = "zau"
                
            };
            var form = new FormCollection(dictionary);


            await controller.Create(form);

            mockRepo.Verify(m => m.CreateAsync(It.IsAny<Recipe>()), Times.Once);
            mockRepo.Verify(m => m.DeleteAsync(It.IsAny<Recipe>()), Times.Never);
            mockRepo.Verify(m => m.GetRecipeAsync(It.IsAny<int>()), Times.Never);
            mockRepo.Verify(m => m.GetRecipesAsDbSet(), Times.Never);
            mockRepo.Verify(m => m.GetRecipesToListAsync(), Times.Never);
            mockRepo.Verify(m => m.UpdateRecipeAsync(It.IsAny<Recipe>()), Times.Never);
            mockRepo.Verify(m => m.RecipeExists(It.IsAny<int>()), Times.Never);


        }

        


    }
}
