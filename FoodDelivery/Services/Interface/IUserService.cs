using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services.Interface
{
    public interface IUserService
    {
        Task<TokenResponse> Register(UserRegisterModel userRegisterModel);
        Task<TokenResponse> Login(LoginCredentials loginCredentials);
        Task Logout();
        Task<string?> AlreadyRegister(UserRegisterModel userRegisterModel);
    }
}
