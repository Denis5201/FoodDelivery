using FoodDelivery.Models.DTO;
using FoodDelivery.Services.Interface;
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
        [Produces("application/json")]
        public async Task<ActionResult<TokenResponse>> PostRegister(UserRegisterModel userRegisterModel)
        {
            string? error = await _userService.AlreadyRegister(userRegisterModel);
            if (error != null)
            {
                return BadRequest(error);
            }

            TokenResponse token = await _userService.Register(userRegisterModel);
            return Ok(token);
        }

        [HttpPost("login")]
        [Produces("application/json")]
        public async Task<ActionResult<TokenResponse>> PostLogin(LoginCredentials loginCredentials)
        {
            TokenResponse token = await _userService.Login(loginCredentials);
            return Ok(token);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> PostLogout()
        {
            await _userService.Logout();
            return Ok();
        }

        [HttpGet("profile")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            var profile = await _userProfileService.GetProfile(User.Identity!.Name!);
            return Ok(profile);
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> PutProfile(UserEditModel editModel)
        {
            await _userProfileService.ChangeProfile(editModel, User.Identity!.Name!);
            return Ok();
        }
    }
}
