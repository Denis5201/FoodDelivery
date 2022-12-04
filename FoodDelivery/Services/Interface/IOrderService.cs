using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services.Interface
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderInfo(Guid id, string userId);
        Task<List<OrderInfoDto>> GetOrderList(string userId);
        Task CreateOrder(OrderCreateDto order, string userId);
        Task ConfirmOrder(Guid id, string userId);
    }
}
