using FoodDelivery.Models;

namespace FoodDelivery.Services
{
    public class DishRatingService : IDishRatingService
    {
        private readonly DatabaseContext _context;

        public DishRatingService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task IsAbleSetRating(Guid id)
        {

        }

        public async Task SetRating(Guid id, int ratingScore)
        {

        }
    }
}
