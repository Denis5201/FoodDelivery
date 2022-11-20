using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder()
        {
            return Ok();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOrderList()
        {
            return Ok();
        }

        [HttpPost("")]
        public async Task<IActionResult> PostOrder()
        {
            return Ok();
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> PostOrderConfirm()
        {
            return Ok();
        }
    }
}
