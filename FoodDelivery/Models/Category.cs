using System.Text.Json.Serialization;

namespace FoodDelivery.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Category
    {
        Wok,
        Pizza,
        Soup,
        Dessert,
        Drink
    }
}
