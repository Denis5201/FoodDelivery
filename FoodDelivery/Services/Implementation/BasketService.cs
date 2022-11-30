using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using FoodDelivery.Models.Entity;
using FoodDelivery.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FoodDelivery.Services.Implementation
{
    public class BasketService : IBasketService
    {
        private readonly DatabaseContext _context;

        public BasketService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<DishBasketDto>> GetBasket(string userId)
        {
            var dishesInBasket = await _context.Baskets
                .Include(u => u.User)
                .Where(u => u.User.Id == Guid.Parse(userId))
                .Include(d => d.Dish)
                .Select(d => new DishBasketDto 
                { 
                    Id = d.Dish.Id,
                    Name = d.Dish.Name,
                    Price = d.Dish.Price,
                    TotalPrice = d.Amount * d.Dish.Price,
                    Amount = d.Amount,
                    Image = d.Dish.Image
                })
                .ToListAsync();
            return dishesInBasket;
        }

        public async Task AddDishInBasket(Guid dishId, string userId)
        {
            var dishInBasket = await _context.Baskets
                .Include(u => u.User)
                .Where(u => u.User.Id == Guid.Parse(userId))
                .Include(d => d.Dish)
                .SingleOrDefaultAsync(d => d.Dish.Id == dishId);

            if (dishInBasket != null)
            {
                dishInBasket.Amount++;
            }
            else
            {
                User user = await _context.Users.SingleAsync(u => u.Id == Guid.Parse(userId));
                Dish dish = await _context.Dishes.SingleAsync(d => d.Id == dishId);

                if (dish == null || user == null) 
                {
                    return;
                }
                var newDish = new DishInBasket
                {
                    Amount = 1,
                    User = user,
                    Dish = dish
                };
                await _context.Baskets.AddAsync(newDish);
                _context.SaveChanges();
            }
        }

        public async Task RemoveDishFromBasket(Guid dishId, bool increase, string userId)
        {
            var dishInBasket = await _context.Baskets
                    .Include(u => u.User)
                    .Where(u => u.User.Id == Guid.Parse(userId))
                    .Include(d => d.Dish)
                    .SingleAsync(d => d.Dish.Id == dishId);

            if (dishInBasket == null)
            {
                return;
            }

            if (!increase || dishInBasket.Amount == 1)
            {
                _context.Baskets.Remove(dishInBasket);
            }
            else
            {
                dishInBasket.Amount--;
            }
            _context.SaveChanges();
        }
    }
}
