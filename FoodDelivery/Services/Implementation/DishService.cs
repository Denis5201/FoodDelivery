using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using FoodDelivery.Models.Entity;
using FoodDelivery.Services.Interface;
using FoodDelivery.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services.Implementation
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
            IQueryable<Dish> dishQuery;
            int count = 0;

            if (categories.Any() && vegetarian)
            {
                dishQuery = _context.Dishes
                    .Where(d => categories.Contains(d.Category) && d.Vegetarian == true)
                    .Include(r => r.Rating);
            }
            else if (categories.Any() && !vegetarian)
            {
                dishQuery = _context.Dishes
                    .Where(d => categories.Contains(d.Category))
                    .Include(r => r.Rating);
            }
            else if (vegetarian)
            {
                dishQuery = _context.Dishes
                    .Where(d => d.Vegetarian == true)
                    .Include(r => r.Rating);
            }
            else
            {
                dishQuery = _context.Dishes
                    .Include(r => r.Rating);
            }
            count = dishQuery.Count();

            if (sorting != null)
            {
                dishQuery = SortingDishes(dishQuery, (DishSorting)sorting);
            }

            var dishList = await dishQuery.Skip((page - 1) * 6)
                    .Take(6)
                    .Select(d => GetDishDtoFromDishes(d))
                    .ToListAsync();

            if (!dishList.Any() && page > 1)
            {
                throw new IncorrectDataException("Слишком большой номер страницы");
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
                }).SingleOrDefaultAsync();

            if (dish == null)
            {
                throw new ItemNotFoundException("Еда не найдена");
            }

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

        private static IQueryable<Dish> SortingDishes(IQueryable<Dish> dishQuery, DishSorting sorting)
        {
            switch (sorting)
            {
                case DishSorting.NameAsc:
                    dishQuery = dishQuery.OrderBy(d => d.Name/*, StringComparer.Ordinal*/);
                    break;
                case DishSorting.NameDesc:
                    dishQuery = dishQuery.OrderByDescending(d => d.Name/*, StringComparer.Ordinal*/);
                    break;
                case DishSorting.PriceAsc:
                    dishQuery = dishQuery.OrderBy(d => d.Price);
                    break;
                case DishSorting.PriceDesc:
                    dishQuery = dishQuery.OrderByDescending(d => d.Price);
                    break;
                case DishSorting.RatingAsc:
                    dishQuery = dishQuery
                        .OrderByDescending(d => d.Rating != null)
                        .ThenBy(d => d.Rating);
                    break;
                case DishSorting.RatingDesc:
                    dishQuery = dishQuery
                        .OrderByDescending(d => d.Rating != null)
                        .OrderByDescending(d => d.Rating);
                    break;
            }
            return dishQuery;
        }
    }
}
