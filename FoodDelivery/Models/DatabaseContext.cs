using FoodDelivery.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodDelivery.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<InvalidToken> InvalidTokens { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>().HasOne(r => r.Rating).WithOne(d => d.Dish).HasForeignKey<Rating>(k => k.DishId);

            modelBuilder.Entity<User>().HasMany(b => b.DishInBasket).WithOne(u => u.User);
            modelBuilder.Entity<User>().HasMany(o => o.Orders).WithOne(u => u.User);
            modelBuilder.Entity<UserRating>().HasKey(k => new { k.UserId, k.RatingId });
            modelBuilder.Entity<UserRating>().HasIndex(k => new { k.UserId, k.RatingId, k.Score }).IsUnique();

            modelBuilder.Entity<User>().Property(p => p.Gender).HasConversion<string>();
            modelBuilder.Entity<Order>().Property(p => p.Status).HasConversion<string>();
            modelBuilder.Entity<Dish>().Property(p => p.Category).HasConversion<string>();
        }
    }
}
