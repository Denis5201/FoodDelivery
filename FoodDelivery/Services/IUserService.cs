using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services
{
    public interface IUserService
    {
        Task<TokenResponse> Register(UserRegisterModel userRegisterModel);
        Task<TokenResponse> Login(LoginCredentials loginCredentials);
        Task Logout();
    }
}
