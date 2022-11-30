using FoodDelivery.Models.DTO;
using FoodDelivery.Services.Interface;
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
        public async Task<ActionResult<List<DishBasketDto>>> GetUserBasket()
        {
            var dishesInBasket = await _basketService.GetBasket(User.Identity!.Name!);
            return Ok(dishesInBasket);
        }

        [HttpPost("dish/{dishId}")]
        [Authorize]
        public async Task<IActionResult> PostDishInBasket(Guid dishId)
        {
            await _basketService.AddDishInBasket(dishId, User.Identity!.Name!);
            return Ok();
        }

        [HttpDelete("dish/{dishId}")]
        [Authorize]
        public async Task<IActionResult> DeleteDishFromBasket(Guid dishId, bool increase = false)
        {
            await _basketService.RemoveDishFromBasket(dishId, increase, User.Identity!.Name!);
            return Ok();
        }
    }
}
