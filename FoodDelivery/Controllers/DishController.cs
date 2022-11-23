﻿using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
    [Route("api/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private IDishService _dishService;
        private IDishRatingService _dishRatingService;

        public DishController(IDishService dishService, IDishRatingService dishRatingService)
        {
            _dishService = dishService;
            _dishRatingService = dishRatingService;
        }

        [HttpGet("")]
        [Produces("application/json")]
        public async Task<ActionResult> GetDishList(Category? category, bool? vegetarian = false, DishSorting? sorting = null, int? page = 1)
        {
            await _dishService.GetDishList();
            return Ok();
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<DishDto>> GetDishInfo(Guid id)
        {
            var diahInfo = await _dishService.GetDishInfo(id);
            return Ok(diahInfo);
        }

        [HttpGet("{id}/rating/check")]
        [Authorize]
        public async Task<ActionResult> GetIsUserHaveAlreadyDish(Guid id)
        {
            await _dishRatingService.IsAbleSetRating(id);
            return Ok();
        }

        [HttpPost("{id}/rating")]
        [Authorize]
        public async Task<IActionResult> PostDishRating(Guid id, int ratingScore)
        {
            await _dishRatingService.SetRating(id, ratingScore);
            return Ok();
        }
    }
}
