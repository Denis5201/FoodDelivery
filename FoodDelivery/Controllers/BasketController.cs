using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetUserBasket()
        {
            return Ok();
        }

        [HttpPost("dish/{dishId}")]
        public async Task<IActionResult> PostDishInBasket()
        {
            return Ok();
        }

        [HttpDelete("dish/{dishId}")]
        public async Task<IActionResult> DeleteDishFromBasket()
        {
            return Ok();
        }
    }
}
