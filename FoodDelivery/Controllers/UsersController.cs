using FoodDelivery.Models.DTO;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IUserProfileService _userProfileService;

        public UsersController(IUserService userService, IUserProfileService userProfileService)
        {
            _userService = userService;
            _userProfileService = userProfileService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostRegister(UserRegisterModel userRegisterModel)
        {
            await _userService.Register(userRegisterModel);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(LoginCredentials loginCredentials)
        {
            TokenResponse t = await _userService.Login(loginCredentials);
            return Ok(t);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> PostLogout()
        {
            await _userService.Logout();
            return Ok();
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            await _userProfileService.GetProfile();
            return Ok();
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> PutProfile(UserEditModel editModel)
        {
            await _userProfileService.ChangeProfile(editModel);
            return Ok();
        }
    }
}
