using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using FoodDelivery.Models.Entity;
using FoodDelivery.Services.Interface;
using FoodDelivery.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _context;

        public OrderService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> GetOrderInfo(Guid id, string userId)
        {
            var orderInfo = await _context.Orders
                .Where(o => o.Id == id)
                .Include(od => od.OrderDishes)
                .ThenInclude(d => d.Dish)
                .Include(u => u.User)
                .SingleOrDefaultAsync();
            
            if (orderInfo == null)
            {
                throw new ItemNotFoundException("Заказ не найден");
            }
            if (orderInfo.User.Id != Guid.Parse(userId))
            {
                throw new NoPermissionException("Доступ запрещён");
            }

            var result = new OrderDto
            {
                Id = id,
                DeliveryTime = orderInfo.DeliveryTime,
                OrderTime = orderInfo.OrderTime,
                Status = orderInfo.Status,
                Price = orderInfo.Price,
                Dishes = orderInfo.OrderDishes.Select(d => new DishBasketDto
                {
                    Id = d.DishesId,
                    Name = d.Dish.Name,
                    Price = d.Dish.Price,
                    TotalPrice = d.Dish.Price * d.Amount,
                    Amount = d.Amount,
                    Image = d.Dish.Image
                }).ToList(),
                Address = orderInfo.Address,
            };

            return result;
        }

        public async Task<List<OrderInfoDto>> GetOrderList(string userId)
        {
            var orderList = await _context.Orders
                .Include(u => u.User)
                .Where(u => u.User.Id == Guid.Parse(userId))
                .Select(o => new OrderInfoDto
                {
                    Id = o.Id,
                    DeliveryTime = o.DeliveryTime,
                    OrderTime = o.OrderTime,
                    Status = o.Status,
                    Price = o.Price,
                })
                .ToListAsync();

            return orderList;
        }

        public async Task CreateOrder(OrderCreateDto order, string userId)
        {
            var infoFromBasket = await _context.Baskets
                .Include(u => u.User)
                .Where(u => u.User.Id == Guid.Parse(userId))
                .Include(d => d.Dish)
                .ToListAsync();

            if (!infoFromBasket.Any())
            {
                throw new IncorrectDataException("Нельзя создать заказ, не выбрав еду");
            }

            User user = await _context.Users.SingleAsync(u => u.Id == Guid.Parse(userId));

            var orderId = Guid.NewGuid();

            var newOrder = new Order
            {
                Id = orderId,
                DeliveryTime = order.DeliveryTime,
                OrderTime = DateTime.Now,
                Status = OrderStatus.InProcess,
                Price = infoFromBasket.Sum(p => p.Amount * p.Dish.Price),
                Address = order.Address,
                User = user
            };
            await _context.Orders.AddAsync(newOrder);

            foreach (var dishInBasket in infoFromBasket)
            {
                _context.DishOrder.Add(new DishOrder
                {
                    OrdersId = orderId,
                    Dish = dishInBasket.Dish,
                    Amount = dishInBasket.Amount
                });
            }

            _context.Baskets.RemoveRange(infoFromBasket);

            _context.SaveChanges();
        }

        public async Task ConfirmOrder(Guid id, string userId)
        {
            var order = await _context.Orders
                .Where(o => o.Id == id)
                .Include(u => u.User)
                .SingleOrDefaultAsync();

            if (order == null)
            {
                throw new ItemNotFoundException("Заказ не найден");
            }
            if (order.User.Id != Guid.Parse(userId))
            {
                throw new NoPermissionException("Доступ запрещён");
            }
            if (order.Status == OrderStatus.Delivered)
            {
                throw new IncorrectDataException("Нельзя подтвердить уже подтверждённый заказ");
            }

            order.Status = OrderStatus.Delivered;
            _context.SaveChanges();
        }
    }
}
