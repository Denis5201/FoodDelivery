namespace FoodDelivery.Services
{
    public interface IDishRatingService
    {
        Task IsAbleSetRating(Guid id);
        Task SetRating(Guid id, int ratingScore);
    }
}
