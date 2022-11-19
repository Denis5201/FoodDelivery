using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        public string FullName { get; set; }

        public DateTime? birthDate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Address { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public ICollection<Basket>? DishInBasket { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
