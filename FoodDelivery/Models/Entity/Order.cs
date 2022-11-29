using System.ComponentModel.DataAnnotations;
using FoodDelivery.Models.DTO;

namespace FoodDelivery.Models.Entity
{
    public class Order
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime DeliveryTime { get; set; }

        [Required]
        public DateTime OrderTime { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MinLength(1)]
        public string Address { get; set; }

        [Required]
        public User User { get; set; }

        public ICollection<Dish> Dishes { get; set; }
    }
}
