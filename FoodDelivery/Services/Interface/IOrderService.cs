using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services.Interface
{
    public interface IOrderService
    {
        Task GetOrderInfo(Guid id);
        Task GetOrderList();
        Task CreateOrder(OrderCreateDto order);
        Task ConfirmOrder(Guid id);
    }
}
