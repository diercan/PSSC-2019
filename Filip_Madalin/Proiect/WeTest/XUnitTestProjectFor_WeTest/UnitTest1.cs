using Microsoft.AspNetCore.Mvc;
using System;
using WeTest.Controllers;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WeTest.Data;
using WeTest.Models;

/// <summary>
///     Unit test for controllers of WeTest mvc app
///         For each controller tested following: 
///            Index()
///            Details()
///     
/// 
/// </summary>
namespace XUnitTestProjectFor_WeTest
{
    public class UnitTest1 
    {
        /*
           Description: Testers Index returns a viewResult
         */
        [Fact(DisplayName = "TestersController Index should return a viewResult")]
        public async Task Testcase_TestersController_Returns_View()
        {
            //arrange
            var testContext = await GetWeTestDbContext();
            TestersController sut = new TestersController(testContext);
            //act
            IActionResult result = await sut.Index();
            //assert
            Assert.IsType<ViewResult>(result);
        }

        /*
           Description: Testers Details returns a not null tester model with ID=2
         */
        [Fact(DisplayName = "TestersController Details should return a not null tester model")]
        public async Task Testcase_TestersController_Returns_TesterModel()
        {
            //arrange
            var testContext = await GetWeTestDbContext();
            TestersController sut = new TestersController(testContext);
            
            //act
            var result =  await sut.Details("2") as ViewResult;
            var modelToTest = result.Model as Tester;

            //assert
            Assert.NotNull(modelToTest);
            Assert.NotNull(modelToTest.TesterName);
            
        }

        /*
           Description: Tests Index returns a viewResult
         */
        [Fact(DisplayName = "TestsController Index should return a viewResult")]
        public async Task Testcase_TestsController_Returns_View()
        {
            //arrange
            var testContext = await GetWeTestDbContext();
            TestersController sut = new TestersController(testContext);
            //act
            IActionResult result = await sut.Index();
            //assert
             Assert.IsType<ViewResult>(result);
        }

        /*
           Description: Tests Details returns a not null test model with ID=8
         */
        [Fact(DisplayName = "TestsController Details should return a not null test model")]
        public async Task Testcase_TestsController_Returns_TestModel()
        {
            //arrange
            var testContext = await GetWeTestDbContext();
            TestsController sut = new TestsController(testContext);

            //act
            var result = await sut.Details("8") as ViewResult;
            var modelToTest = result.Model as Test;

            //assert
            Assert.NotNull(modelToTest);
            Assert.NotNull(modelToTest.TestTitle);

        }


        //get a db context from in memory db
        private async Task<WeTestContext> GetWeTestDbContext()
        {
            var options = new DbContextOptionsBuilder<WeTestContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) //from efcore.InMemory package
                .Options;
            var databaseContext = new WeTestContext(options);
            databaseContext.Database.EnsureCreated();
            
            if (await databaseContext.Tester.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    //add tester
                    databaseContext.Tester.Add(new Tester()
                    {
                        TesterId = i.ToString(),
                        TesterName = $"testerMocked{i}",
                    });
                    await databaseContext.SaveChangesAsync();

                    //add test
                    databaseContext.Test.Add(new Test()
                    {
                        TestId = i.ToString(),
                        TestTitle = $"TestMocked{i}",
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
    }
}
