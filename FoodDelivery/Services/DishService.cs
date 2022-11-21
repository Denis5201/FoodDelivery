using FoodDelivery.Models;

namespace FoodDelivery.Services
{
    public class DishService : IDishService
    {
        private readonly DatabaseContext _context;

        public DishService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task GetDishList()
        {
            
        }

        public async Task GetDishInfo(Guid id)
        {
            
        }
    }
}
