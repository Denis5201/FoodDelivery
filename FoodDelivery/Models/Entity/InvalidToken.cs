using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Entity
{
    public class InvalidToken
    {
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime ExitTime { get; set; }
    }
}
