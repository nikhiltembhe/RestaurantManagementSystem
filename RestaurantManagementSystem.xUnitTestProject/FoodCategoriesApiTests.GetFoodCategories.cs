using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestaurantManagementSystem.Controllers;
using RestaurantManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RestaurantManagementSystem.xUnitTestProject
{
    public partial class FoodCategoriesApiTests
    {
        [Fact]
        public void GetFoodCategories_OkResult()
        {
            // ARRANGE
            var dbName = nameof(FoodCategoriesApiTests.GetFoodCategories_OkResult);
            var logger = Mock.Of<ILogger<FoodCategoriesController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apiController = new FoodCategoriesController(dbContext, logger);

            // ACT
            IActionResult actionResult = apiController.GetFoodCategories().Result;

            // ASSERT
            // --- Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);

            // --- Check if the HTTP Response Code is 200 "Ok"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            int actualStatusCode = (actionResult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetFoodCategories_CheckCorrectResult()
        {
            // ARRANGE
            var dbName = nameof(FoodCategoriesApiTests.GetFoodCategories_CheckCorrectResult);
            var logger = Mock.Of<ILogger<FoodCategoriesController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apiController = new FoodCategoriesController(dbContext, logger);


            // ACT: Call the API action method
            IActionResult actionResult = apiController.GetFoodCategories().Result;

            // ASSERT: Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);



            // ACT: Extract the OkResult result 
            var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT: if the OkResult contains the object of the Correct Type
            Assert.IsAssignableFrom<List<FoodCategory>>(okResult.Value);



            // ACT: Extract the Categories from the result of the action
            var foodCategoriesFromApi = okResult.Value.Should().BeAssignableTo<List<FoodCategory>>().Subject;

            // ASSERT: if the categories is NOT NULL
            Assert.NotNull(foodCategoriesFromApi);

            // ASSERT: if the number of categories in the DbContext seed data
            //         is the same as the number of categories returned in the API Result
            Assert.Equal<int>(expected: DbContextMocker.TestData_Categories.Length,
                              actual: foodCategoriesFromApi.Count);

            // ASSERT: Test the data received from the API against the Seed Data
            int ndx = 0;
            foreach (FoodCategory foodCategory in DbContextMocker.TestData_Categories)
            {
                // ASSERT: check if the Category ID is correct
                Assert.Equal<int>(expected: foodCategory.FoodCategoryId,
                                  actual: foodCategoriesFromApi[ndx].FoodCategoryId);

                // ASSERT: check if the Category Name is correct
                Assert.Equal(expected: foodCategory.FoodCategoryName,
                             actual: foodCategoriesFromApi[ndx].FoodCategoryName);

                _testOutputHelper.WriteLine($"Compared Row # {ndx} successfully");

                ndx++;          // now compare against the next element in the array
            }
        }
    }
}
