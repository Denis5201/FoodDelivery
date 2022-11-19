using System.Text.Json.Serialization;

namespace FoodDelivery.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male,
        Female
    }
}
