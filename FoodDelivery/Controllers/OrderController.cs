using FoodDelivery.Models.DTO;
using FoodDelivery.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            await _orderService.GetOrderInfo(id);
            return Ok();
        }

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetOrderList()
        {
            await _orderService.GetOrderList();
            return Ok();
        }

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> PostOrder(OrderCreateDto order)
        {
            await _orderService.CreateOrder(order);
            return Ok();
        }

        [HttpPost("{id}/status")]
        [Authorize]
        public async Task<IActionResult> PostOrderConfirm(Guid id)
        {
            await _orderService.ConfirmOrder(id);
            return Ok();
        }
    }
}
