using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private IDishService _dishService;
        private IDishRatingService _dishRatingService;

        public DishController(IDishService dishService, IDishRatingService dishRatingService)
        {
            _dishService = dishService;
            _dishRatingService = dishRatingService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetDishList()
        {
            await _dishService.GetDishList();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDishInfo(Guid id)
        {
            await _dishService.GetDishInfo(id);
            return Ok();
        }

        [HttpGet("{id}/rating/check")]
        [Authorize]
        public async Task<IActionResult> GetIsUserHaveAlreadyDish(Guid id)
        {
            await _dishRatingService.IsAbleSetRating(id);
            return Ok();
        }

        [HttpPost("{id}/rating")]
        [Authorize]
        public async Task<IActionResult> PostDishRating(Guid id, int ratingScore)
        {
            await _dishRatingService.SetRating(id, ratingScore);
            return Ok();
        }
    }
}
