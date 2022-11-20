using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetDishList()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDishInfo()
        {
            return Ok();
        }

        [HttpGet("{id}/rating/check")]
        public async Task<IActionResult> GetIsUserHaveAlreadyDish()
        {
            return Ok();
        }

        [HttpPost("{id}/rating")]
        public async Task<IActionResult> PostDishRating()
        {
            return Ok();
        }
    }
}
