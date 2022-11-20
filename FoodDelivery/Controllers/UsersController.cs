using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> PostRegister()
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> PostLogin()
        {
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> PostLogout()
        {
            return Ok();
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            return Ok();
        }

        [HttpPut("profile")]
        public async Task<IActionResult> PutProfile()
        {
            return Ok();
        }
    }
}
