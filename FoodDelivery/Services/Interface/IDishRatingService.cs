namespace FoodDelivery.Services.Interface
{
    public interface IDishRatingService
    {
        Task<bool> IsAbleSetRating(Guid id, string userId);
        Task SetRating(Guid id, int ratingScore, string userId);
    }
}
