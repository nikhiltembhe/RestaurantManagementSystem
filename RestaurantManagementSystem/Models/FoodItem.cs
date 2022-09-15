using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models
{
    [Table("FoodItems")]
    public class FoodItem
    {
        [Key]                                                               //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodItemId { get; set; }

        [Display(Name = "Food Name")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string FoodItemName { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [Range(minimum: 100, maximum: 7000, ErrorMessage = "{0} has to be between {1} and {2}")]
        public float FoodItemPrice { get; set; }

        //[Display(Name = "Quantity")]
        ////[Required(ErrorMessage = "{0} cannot be empty.")]
        //public int FoodItemQuantity { get; set; }


        #region Navigation Properties to the Master Model - FoodCategory


        [Required]
        public int FoodCategoryId { get; set; }


        [ForeignKey(nameof(FoodItem.FoodCategoryId))]
        public FoodCategory FoodCategory { get; set; }


        #endregion


        #region Navigation Properties to the Transaction Model - Order

        public ICollection<Order> Orders { get; set; }

        #endregion
    }
}
