using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services.Interface
{
    public interface IUserProfileService
    {
        Task<UserDto> GetProfile(string id);
        Task ChangeProfile(UserEditModel editModel, string id);
    }
}
