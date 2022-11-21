using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services
{
    public interface IUserProfileService
    {
        Task GetProfile();
        Task ChangeProfile(UserEditModel editModel);
    }
}
