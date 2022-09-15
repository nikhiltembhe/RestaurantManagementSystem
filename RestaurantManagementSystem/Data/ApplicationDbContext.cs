using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FoodCategory> FoodCategories { get; set; }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<RestaurantManagementSystem.Models.Order> Order { get; set; }

        public DbSet<RestaurantManagementSystem.Models.Customer> Customer { get; set; }

        public DbSet<RestaurantManagementSystem.Models.Waiter> Waiter { get; set; }


    }
}
