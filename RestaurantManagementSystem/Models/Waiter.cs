using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagementSystem.Models
{
    [Table("Waiters")]
    public class Waiter
    {
        [Key]                                                               //Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WaiterId { get; set; }

        [Display(Name = "Waiter Name")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string WaiterName { get; set; }


        #region Navigation Properties to the Transaction Model - Order

        public ICollection<Order> Orders { get; set; }

        #endregion

    }
}
