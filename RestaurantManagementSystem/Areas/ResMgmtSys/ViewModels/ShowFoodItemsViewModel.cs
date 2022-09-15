using RestaurantManagementSystem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Areas.ResMgmtSys.ViewModels
{
    public class ShowFoodItemsViewModel
    {
        [Display(Name = "Select Category:")]
        [Required(ErrorMessage = "Please select a category for displaying the Foods")]
        public int FoodCategoryId { get; set; }

        public ICollection<FoodCategory> FoodCategories { get; set; }
    }
}
