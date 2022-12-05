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
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "StillWorkingToken")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        {
            var order = await _orderService.GetOrderInfo(id, User.Identity!.Name!);
            return Ok(order);
        }

        [HttpGet("")]
        [Authorize(Policy = "StillWorkingToken")]
        public async Task<ActionResult<List<OrderInfoDto>>> GetOrderList()
        {
            var orderList = await _orderService.GetOrderList(User.Identity!.Name!);
            return Ok(orderList);
        }

        [HttpPost("")]
        [Authorize(Policy = "StillWorkingToken")]
        public async Task<IActionResult> PostOrder(OrderCreateDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (order.DeliveryTime < DateTime.Now.AddMinutes(20))
            {
                return BadRequest(new Response { Status = "400", Message = "Слишком мало времени на доставку" });
            }
            await _orderService.CreateOrder(order, User.Identity!.Name!);
            return Ok();
        }

        [HttpPost("{id}/status")]
        [Authorize(Policy = "StillWorkingToken")]
        public async Task<IActionResult> PostOrderConfirm(Guid id)
        {
            await _orderService.ConfirmOrder(id, User.Identity!.Name!);
            return Ok();
        }
    }
}
