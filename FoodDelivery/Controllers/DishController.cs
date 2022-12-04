using FoodDelivery.Models.DTO;
using FoodDelivery.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        private readonly IDishRatingService _dishRatingService;

        public DishController(IDishService dishService, IDishRatingService dishRatingService)
        {
            _dishService = dishService;
            _dishRatingService = dishRatingService;
        }

        [HttpGet("")]
        [Produces("application/json")]
        public async Task<ActionResult<DishPagedListDto>> GetDishList(
            [FromQuery] List<Category> categories = null, 
            bool vegetarian = false, 
            DishSorting? sorting = null, 
            int page = 1)
        {
            categories = categories ?? new List<Category>();
            var result = await _dishService.GetDishList(categories, vegetarian, sorting, page);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<DishDto>> GetDishInfo(Guid id)
        {
            var diahInfo = await _dishService.GetDishInfo(id);
            return Ok(diahInfo);
        }

        [HttpGet("{id}/rating/check")]
        [Authorize(Policy = "StillWorkingToken")]
        public async Task<ActionResult<bool>> GetIsUserHaveAlreadyDish(Guid id)
        {
            bool result = await _dishRatingService.IsAbleSetRating(id, User.Identity!.Name!);
            return Ok(result);
        }

        [HttpPost("{id}/rating")]
        [Authorize(Policy = "StillWorkingToken")]
        public async Task<IActionResult> PostDishRating(Guid id, int ratingScore)
        {
            bool isAble = await _dishRatingService.IsAbleSetRating(id, User.Identity!.Name!);
            if (!isAble)
            {
                return BadRequest();
            }
            await _dishRatingService.SetRating(id, ratingScore, User.Identity!.Name!);
            return Ok();
        }
    }
}
