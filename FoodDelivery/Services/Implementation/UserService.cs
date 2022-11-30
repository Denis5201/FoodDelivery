using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using FoodDelivery.Models.Entity;
using FoodDelivery.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FoodDelivery.Services.Implementation
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
            Guid newId = Guid.NewGuid();
            await _context.Users.AddAsync(new User
            {
                Id = newId,
                FullName = userRegisterModel.FullName,
                Password = userRegisterModel.Password,
                Email = userRegisterModel.Email,
                Address = userRegisterModel.Address,
                BirthDate = userRegisterModel.BirthDate,
                Gender = userRegisterModel.Gender,
                PhoneNumber = userRegisterModel.PhoneNumber
            });
            await _context.SaveChangesAsync();

            string token = CreateToken(newId);
            return new TokenResponse { Token = token };
        }

        public async Task<TokenResponse> Login(LoginCredentials loginCredentials)
        {
            var user = await _context.Users
                .Where(u => u.Email == loginCredentials.Email && u.Password == loginCredentials.Password)
                .SingleOrDefaultAsync();
            if (user == null)
            {
                return new TokenResponse { Token = null };
            }

            string token = CreateToken(user.Id);
            return new TokenResponse { Token = token };
        }

        public async Task Logout(string token)
        {
            var invalidToken = new InvalidToken 
            { 
                Token = token,
                ExitTime = DateTime.Now
            };
            await _context.InvalidTokens.AddAsync(invalidToken);
            _context.SaveChanges();
        }

        public async Task<string?> AlreadyRegister(UserRegisterModel userRegisterModel)
        {
            var user = await _context.Users.Where(e => e.Email == userRegisterModel.Email).SingleOrDefaultAsync();
            if (user != null)
            {
                return "Пользователь с данным E-mail уже существует";
            }
            return null;
        }

        private string CreateToken(Guid id)
        {
            var claim = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, id.ToString()) };
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: JwtConfigs.Issuer,
                audience: JwtConfigs.Audience,
                notBefore: now,
                claims: claim,
                expires: now.AddMinutes(JwtConfigs.Lifetime),
                signingCredentials: new SigningCredentials(JwtConfigs.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
