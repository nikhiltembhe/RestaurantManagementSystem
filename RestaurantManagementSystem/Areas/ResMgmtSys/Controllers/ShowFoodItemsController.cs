using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RestaurantManagementSystem.Areas.ResMgmtSys.ViewModels;
using RestaurantManagementSystem.Data;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManagementSystem.Areas.ResMgmtSys.Controllers
{
    [Area("ResMgmtSys")]
    public class ShowFoodItemsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ShowFoodItemsController> _logger;

        public ShowFoodItemsController(ApplicationDbContext dbContext, ILogger<ShowFoodItemsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            PopulateDropDownListToSelectCategory();

            _logger.LogInformation("--- extracted Categories");
            return View();
        }

        private void PopulateDropDownListToSelectCategory()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem
            {
                Text = "----- select a category -----",
                Value = "",
                Selected = true
            });
            categories.AddRange(new SelectList(_dbContext.FoodCategories, "FoodCategoryId", "FoodCategoryName"));

            ViewData["CategoriesCollection"] = categories;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("FoodCategoryId")] ShowFoodItemsViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                // Something is wrong with the viewmodel.  So, just return it back to the view with the ModelState errors!
                return View(viewmodel);
            }


            // Now performing server-side validation - checking if any books exist for the selected category
            bool fooditemsExist = _dbContext.FoodItems.Any(b => b.FoodCategoryId == viewmodel.FoodCategoryId);
            if (!fooditemsExist)
            {
                //--- Error will be shown as part of the Validation Summary
                ModelState.AddModelError("", "No FoodItems were found for the selected category!");

                //--- Error will be attached to the UI Control mapped by the asp-for attribute.
                // ModelState.AddModelError("CategoryId", "No books were found for this category");

                PopulateDropDownListToSelectCategory();

                return View(viewmodel);         // return the viewmodel with the ModelState errors!
            }


            return RedirectToAction(
                actionName: "GetFoodsOfCategory",
                controllerName: "FoodItems",
                routeValues: new { area = "ResMgmtSys", filterFoodCategoryId = viewmodel.FoodCategoryId });


        }
    }
}
