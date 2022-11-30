using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services.Interface
{
    public interface IBasketService
    {
        Task<List<DishBasketDto>> GetBasket(string userId);
        Task AddDishInBasket(Guid dishId, string userId);
        Task RemoveDishFromBasket(Guid dishId, bool increase, string userId);
    }
}
