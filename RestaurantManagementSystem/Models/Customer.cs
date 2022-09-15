using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]                                                               //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }


        [Display(Name = "Customer Full Name")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        [RegularExpression(@"^[A-Za-z]+[\s][A-Za-z]+$", ErrorMessage ="Use only characters!")]
        public string CustomerName { get; set; }


        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter only 10 digits")]
        public string PhoneNumer { get; set; }


        [EmailAddress(ErrorMessage = "{0} is not valid.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} cannot be empty")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string Address { get; set; }



        #region Navigation Properties to the Transaction Model - Order

        public ICollection<Order> Orders { get; set; }

        #endregion
    }
}
