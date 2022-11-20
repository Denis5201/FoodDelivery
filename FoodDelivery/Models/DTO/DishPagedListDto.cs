namespace FoodDelivery.Models.DTO
{
    public class DishPagedListDto
    {
        public List<DishDto>? Dishes { get; set; }

        public PageInfoModel Pagination { get; set; }
    }
}
