using FoodDelivery.Models;
using FoodDelivery.Services.Interface;
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

        public async Task GetBasket()
        {

        }

        public async Task AddDishInBasket(Guid dishId)
        {

        }

        public async Task RemoveDishFromBasket(Guid dishId)
        {

        }
    }
}
