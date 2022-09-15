using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models
{
    [Table("FoodCategories")]
    public class FoodCategory
    {
        [Key]                                                               //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodCategoryId { get; set; }


        [Display(Name = "Food Category")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string FoodCategoryName { get; set; }


        #region Navigation Properties to the Transaction Model - FoodItem

        public ICollection<FoodItem> FoodItems { get; set; }

        #endregion
    }
}
