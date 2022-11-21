namespace FoodDelivery.Services
{
    public interface IBasketService
    {
        Task GetBasket();
        Task AddDishInBasket(Guid dishId);
        Task RemoveDishFromBasket(Guid dishId);
    }
}
