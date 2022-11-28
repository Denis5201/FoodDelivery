using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services
{
    public class DishService : IDishService
    {
        private readonly DatabaseContext _context;

        public DishService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<DishPagedListDto> GetDishList(List<Category> categories, bool vegetarian, DishSorting? sorting, int page)
        {
            List<DishDto> dishList = new();
            int count = 0;

            if (categories.Any() && vegetarian) 
            {
                dishList = await _context.Dishes
                    .Where(d => categories.Contains(d.Category) && d.Vegetarian == true)
                    .Skip((page - 1) * 6)
                    .Take(6)
                    .Include(r => r.Rating)
                    .Select(d => GetDishDtoFromDishes(d))
                    .ToListAsync();
                count = _context.Dishes
                    .Where(d => categories.Contains(d.Category) && d.Vegetarian == true).Count();
            }
            else if (categories.Any() && !vegetarian)
            {
                dishList = await _context.Dishes
                    .Where(d => categories.Contains(d.Category))
                    .Skip((page - 1) * 6)
                    .Take(6)
                    .Include(r => r.Rating)
                    .Select(d => GetDishDtoFromDishes(d))
                    .ToListAsync();
                count = _context.Dishes.Where(d => categories.Contains(d.Category)).Count();
            }
            else if (vegetarian) 
            {
                dishList = await _context.Dishes
                    .Where(d => d.Vegetarian == true)
                    .Skip((page - 1) * 6)
                    .Take(6)
                    .Include(r => r.Rating)
                    .Select(d => GetDishDtoFromDishes(d))
                    .ToListAsync();
                count = _context.Dishes.Where(d => d.Vegetarian == true).Count();
            }
            else
            {
                dishList = await _context.Dishes
                    .Skip((page - 1) * 6)
                    .Take(6)
                    .Include(r => r.Rating)
                    .Select(d => GetDishDtoFromDishes(d))
                    .ToListAsync();
                count = _context.Dishes.Count();
            }

            if (dishList.Any() && sorting != null)
            {
                dishList = SortingDishes(dishList, (DishSorting)sorting);
            }

            return new DishPagedListDto
            {
                Dishes = dishList,
                Pagination = new PageInfoModel
                {
                    Size = 6,
                    Count = (int)Math.Ceiling(count / 6.0),
                    Current = page
                }
            };
        }

        public async Task<DishDto> GetDishInfo(Guid id)
        {
            var dish = await _context.Dishes
                .Include(r => r.Rating)
                .Where(i => i.Id == id)
                .Select(p => new DishDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Image = p.Image,
                    Vegetarian = p.Vegetarian,
                    Rating = p.Rating == null ? null : p.Rating.DishRating,
                    Category = p.Category
                }).SingleAsync();
            return dish;
        }

        private static DishDto GetDishDtoFromDishes(Dish dish) 
        {
            return new DishDto
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Image = dish.Image,
                Vegetarian = dish.Vegetarian,
                Rating = dish.Rating == null ? null : dish.Rating.DishRating,
                Category = dish.Category
            };
        }

        private List<DishDto> SortingDishes(List<DishDto> dishList, DishSorting sorting) 
        {
            switch(sorting) 
            {
                case DishSorting.NameAsc:
                    dishList = dishList.OrderBy(d => d.Name, StringComparer.Ordinal).ToList();
                    break;
                case DishSorting.NameDesc:
                    dishList = dishList.OrderByDescending(d => d.Name, StringComparer.Ordinal).ToList();
                    break;
                case DishSorting.PriceAsc:
                    dishList = dishList.OrderBy(d => d.Price).ToList();
                    break;
                case DishSorting.PriceDesc:
                    dishList = dishList.OrderByDescending(d => d.Price).ToList();
                    break;
                case DishSorting.RatingAsc:
                    dishList = dishList
                        .OrderByDescending(d => d.Rating.HasValue)
                        .ThenBy(d => d.Rating).ToList();
                    break;
                case DishSorting.RatingDesc:
                    dishList = dishList
                        .OrderByDescending(d => d.Rating.HasValue)
                        .OrderByDescending(d => d.Rating).ToList();
                    break;
            }
            return dishList;
        }
    }
}
