using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services.Interface
{
    public interface IDishService
    {
        Task<DishPagedListDto> GetDishList(List<Category> categories, bool vegetarian, DishSorting? sorting, int page);
        Task<DishDto> GetDishInfo(Guid id);
    }
}
