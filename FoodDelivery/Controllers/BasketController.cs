using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetUserBasket()
        {
            await _basketService.GetBasket();
            return Ok(User.Identity.Name);
        }

        [HttpPost("dish/{dishId}")]
        [Authorize]
        public async Task<IActionResult> PostDishInBasket(Guid dishId)
        {
            await _basketService.AddDishInBasket(dishId);
            return Ok();
        }

        [HttpDelete("dish/{dishId}")]
        [Authorize]
        public async Task<IActionResult> DeleteDishFromBasket(Guid dishId)
        {
            await _basketService.RemoveDishFromBasket(dishId);
            return Ok();
        }
    }
}
