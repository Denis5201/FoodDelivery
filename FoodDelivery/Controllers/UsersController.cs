﻿using FoodDelivery.Models.DTO;
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
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;

        public UsersController(IUserService userService, IUserProfileService userProfileService)
        {
            _userService = userService;
            _userProfileService = userProfileService;
        }

        [HttpPost("register")]
        [Produces("application/json")]
        public async Task<ActionResult<TokenResponse>> PostRegister(UserRegisterModel userRegisterModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.AlreadyRegister(userRegisterModel);

            if (userRegisterModel.BirthDate >= DateTime.Now)
            {
                return BadRequest(new Response { Status = "400", Message = "Недопустимая дата рождения" });
            }

            TokenResponse token = await _userService.Register(userRegisterModel);
            return Ok(token);
        }

        [HttpPost("login")]
        [Produces("application/json")]
        public async Task<ActionResult<TokenResponse>> PostLogin(LoginCredentials loginCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TokenResponse token = await _userService.Login(loginCredentials);
            return Ok(token);
        }

        [HttpPost("logout")]
        [Authorize(Policy = "StillWorkingToken")]
        public async Task<IActionResult> PostLogout()
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            await _userService.Logout(token);
            return Ok();
        }

        [HttpGet("profile")]
        [Produces("application/json")]
        [Authorize(Policy = "StillWorkingToken")]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            var profile = await _userProfileService.GetProfile(User.Identity!.Name!);
            return Ok(profile);
        }

        [HttpPut("profile")]
        [Authorize(Policy = "StillWorkingToken")]
        public async Task<IActionResult> PutProfile(UserEditModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (editModel.BirthDate >= DateTime.Now)
            {
                return BadRequest(new Response { Status = "400", Message = "Недопустимая дата рождения" });
            }

            await _userProfileService.ChangeProfile(editModel, User.Identity!.Name!);
            return Ok();
        }
    }
}
