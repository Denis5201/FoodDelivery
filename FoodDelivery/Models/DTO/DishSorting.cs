using System.Text.Json.Serialization;

namespace FoodDelivery.Models.DTO
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DishSorting
    {
        NameAsc, 
        NameDesc, 
        PriceAsc, 
        PriceDesc, 
        RatingAsc, 
        RatingDesc
    }
}
