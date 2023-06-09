﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Entity
{
    public class DishInBasket
    {
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Dish Dish { get; set; }
    }
}
