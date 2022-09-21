using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagementSystem.xUnitTestProject
{
    public static class DbContextMocker
    {
        public static ApplicationDbContext GetApplicationDbContext(string dbName)
        {
            // Create a fresh service provider for the InMemory Database instance
            var serviceProvider = new ServiceCollection()
                                  .AddEntityFrameworkInMemoryDatabase()
                                  .BuildServiceProvider();

            // Create a new options instance telling the context
            // to use InMemory database with the new service provider created above
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                           .UseInMemoryDatabase(dbName)
                           .UseInternalServiceProvider(serviceProvider)
                           .Options;

            // Create the instance of the DbContext
            var dbContext = new ApplicationDbContext(options);

            // Add entities to the InMemory Database to seed the test data
            dbContext.SeedData();

            return dbContext;
        }


        // NOTE: InMemory Databases do not support Relationships, and Database Constraints properly.
        //       So, seed the Identity Column Values also.
        internal static readonly FoodCategory[] TestData_Categories
            = {
                new FoodCategory
                {
                    FoodCategoryId = 1,
                    FoodCategoryName = "First Category"
                },
                new FoodCategory
                {
                    FoodCategoryId = 2,
                    FoodCategoryName = "Second Category"
                },
                new FoodCategory
                {
                    FoodCategoryId = 3,
                    FoodCategoryName = "Third Category"
                },
            };


        /// <summary>
        ///     An extension Method for the DbContext object to Seed the Test Data.
        /// </summary>
        /// <param name="context">Application Db Context object.</param>
        private static void SeedData(this ApplicationDbContext context)
        {
            context.FoodCategories.AddRange(TestData_Categories);

            // Commit the Changes to the database
            context.SaveChanges();
        }
    }
}
