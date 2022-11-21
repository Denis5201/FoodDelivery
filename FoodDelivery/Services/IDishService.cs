namespace FoodDelivery.Services
{
    public interface IDishService
    {
        Task GetDishList();
        Task GetDishInfo(Guid id);
    }
}
