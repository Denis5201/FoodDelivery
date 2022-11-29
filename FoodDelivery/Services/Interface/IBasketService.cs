namespace FoodDelivery.Services.Interface
{
    public interface IBasketService
    {
        Task GetBasket();
        Task AddDishInBasket(Guid dishId);
        Task RemoveDishFromBasket(Guid dishId);
    }
}
