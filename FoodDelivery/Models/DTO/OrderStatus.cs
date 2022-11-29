using System.Text.Json.Serialization;

namespace FoodDelivery.Models.DTO
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        InProcess,
        Delivered
    }
}
