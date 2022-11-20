using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.DTO
{
    public class OrderCreateDto
    {
        [Required]
        public DateTime DeliveryTime { get; set; }

        [Required]
        [MinLength(1)]
        public string Address { get; set; }
    }
}
