using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Entity
{
    public class UserRating
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int RatingId { get; }

        [Required]
        public User User { get; set; }

        [Required]
        public Rating Rating { get; set; }

        [Required]
        public int Score { get; set; }
    }
}
