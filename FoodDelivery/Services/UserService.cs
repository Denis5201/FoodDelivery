using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;

namespace FoodDelivery.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<TokenResponse> Register(UserRegisterModel userRegisterModel)
        {
            return new TokenResponse { Token = "123" };
        }

        public async Task<TokenResponse> Login(LoginCredentials loginCredentials)
        {
            var claim = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, loginCredentials.Email) };
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: JwtConfigs.Issuer,
                audience: JwtConfigs.Audience,
                notBefore: now,
                claims: claim,
                expires: now.AddMinutes(JwtConfigs.Lifetime),
                signingCredentials: new SigningCredentials(JwtConfigs.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            return new TokenResponse { Token = new JwtSecurityTokenHandler().WriteToken(jwt) };
        }

        public async Task Logout()
        {

        }
    }
}
