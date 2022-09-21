using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace RestaurantManagementSystem.xUnitTestProject
{
    public partial class FoodCategoriesApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public FoodCategoriesApiTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}
