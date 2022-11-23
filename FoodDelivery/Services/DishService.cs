using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using Microsoft.EntityFrameworkCore;

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

        public async Task<DishDto> GetDishInfo(Guid id)
        {
            var dish = await _context.Dishes
                .Include(r => r.Rating)
                .Where(i => i.Id == id)
                .Select(p => new DishDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Image = p.Image,
                    Vegetarian = p.Vegetarian,
                    Rating = p.Rating == null ? null : p.Rating.DishRating
                }).SingleAsync();
            return dish;
        }
    }
}
