using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Entity
{
    public class DishOrder
    {
        [Required]
        public Guid DishesId { get; set; }

        [Required]
        public Guid OrdersId { get; set; }

        [Required]
        public int Amount { get; set; }

        public Order Order { get; set; }

        public Dish Dish { get; set; }
    }
}
