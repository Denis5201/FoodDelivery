using System.Text.Json.Serialization;

namespace FoodDelivery.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        InProcess,
        Delivered
    }
}
