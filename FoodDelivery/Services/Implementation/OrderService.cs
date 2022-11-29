using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using FoodDelivery.Services.Interface;

namespace FoodDelivery.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _context;

        public OrderService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task GetOrderInfo(Guid id)
        {

        }

        public async Task GetOrderList()
        {

        }

        public async Task CreateOrder(OrderCreateDto order)
        {

        }

        public async Task ConfirmOrder(Guid id)
        {

        }
    }
}
