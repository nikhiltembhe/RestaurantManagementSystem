using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]                                                               //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }


        [Display(Name = "Customized your order ")]
        //[Required(ErrorMessage = "{0} cannot be empty")]
        [StringLength(100, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string OrderName { get; set; }

        [Display(Name = "Order was created on")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;


        [Display(Name = "Order Completed")]
        public bool OrderCompleted { get; set; }


        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [DefaultValue(1)]
        public int OrderQuantity { get; set; } = 1;


        #region Navigation Properties to the Master Model - FoodItem


        [Required]
        public int FoodItemId { get; set; }

        [ForeignKey(nameof(Order.FoodItemId))]
        public FoodItem FoodItem { get; set; }


        #endregion

        #region Navigation Properties to the Master Model - Customer


        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(Order.CustomerId))]
        public Customer Customer { get; set; }


        #endregion

        #region Navigation Properties to the Master Model - Waiter


        [Required]
        public int WaiterId { get; set; }

        [ForeignKey(nameof(Order.WaiterId))]
        public Waiter Waiter { get; set; }


        #endregion
    }
}
