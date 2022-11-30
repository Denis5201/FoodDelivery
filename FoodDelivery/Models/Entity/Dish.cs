using System.ComponentModel.DataAnnotations;
using FoodDelivery.Models.DTO;

namespace FoodDelivery.Models.Entity
{
    public class Dish
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        public string? Image { get; set; }

        public bool Vegetarian { get; set; }

        public Rating? Rating { get; set; }

        public Category Category { get; set; }

        public ICollection<DishOrder>? OrderDishes { get; set; }
    }
}
