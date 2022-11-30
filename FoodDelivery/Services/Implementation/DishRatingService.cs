using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using FoodDelivery.Models.Entity;
using FoodDelivery.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services.Implementation
{
    public class DishRatingService : IDishRatingService
    {
        private readonly DatabaseContext _context;

        public DishRatingService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> IsAbleSetRating(Guid id, string userId)
        {
            bool result = await _context.Orders
                .Where(o => o.Status == OrderStatus.Delivered)
                .Include(u => u.User)
                .Where(u => u.User.Id == Guid.Parse(userId))
                .Include(d => d.OrderDishes)
                .Where(d => d.OrderDishes.Any(c => c.DishesId == id))
                .AnyAsync();
            return result;
        }

        public async Task SetRating(Guid id, int ratingScore, string userId)
        {
            var userGuid = Guid.Parse(userId);

            bool dishAlreadyHasRating = await _context.Rating.AnyAsync(r => r.DishId == id);
            User user = await _context.Users.SingleAsync(u => u.Id == userGuid);

            if (dishAlreadyHasRating)
            {
                var rating = await _context.Rating
                    .Where(r => r.DishId == id)
                    .Include(ur => ur.UserRatings)
                    .ThenInclude(u => u.User)
                    .SingleAsync();
                int count = rating.UserRatings.Count;
                bool userAlreadySetsMark = rating.UserRatings.Any(u => u.User.Id == userGuid);

                if (userAlreadySetsMark)
                {
                    rating.DishRating = (rating.DishRating * count
                        - rating.UserRatings.Single(u => u.User.Id == userGuid).Score + ratingScore) / count;

                    rating.UserRatings.Single(u => u.UserId == userGuid).Score = ratingScore;
                }
                else
                {
                    rating.DishRating = (rating.DishRating * count + ratingScore) / (count + 1);
                    _context.UserRatings.Add(new UserRating
                    {
                        User = user,
                        Rating = rating,
                        Score = ratingScore
                    });
                }
            }
            else
            {
                var rating = new Rating
                {
                    DishRating = ratingScore,
                    DishId = id
                };
                _context.Rating.Add(rating);
                _context.UserRatings.Add(new UserRating
                {
                    User = user,
                    Rating = rating,
                    Score = ratingScore
                });
            }
            _context.SaveChanges();
        }
    }
}
