using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services
{
    public interface IDishService
    {
        Task GetDishList();
        Task<DishDto> GetDishInfo(Guid id);
    }
}
