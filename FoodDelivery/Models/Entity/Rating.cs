using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Entity
{
    public class Rating
    {
        public int Id { get; set; }

        public double DishRating { get; set; }

        [Required]
        public Guid DishId { get; set; }

        [Required]
        public Dish Dish { get; set; }

        [Required]
        public ICollection<UserRating> UserRatings { get; set; }
    }
}
