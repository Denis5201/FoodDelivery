using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public double DishRating { get; set; }

        public int AmountVotes { get; set; }

        [Required]
        public Guid DishId { get; set; }

        [Required]
        public Dish Dish { get; set; }
    }
}
