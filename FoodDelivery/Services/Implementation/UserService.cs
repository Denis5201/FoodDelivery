using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using FoodDelivery.Models.Entity;
using FoodDelivery.Services.Exceptions;
using FoodDelivery.Services.Interface;
using Microsoft.AspNetCore.Identity;
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
            var hasher = new PasswordHasher<User>();
            var hashPassword = hasher.HashPassword(new User(), userRegisterModel.Password);

            await _context.Users.AddAsync(new User
            {
                Id = newId,
                FullName = userRegisterModel.FullName,
                Password = hashPassword,
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

        public async Task AlreadyRegister(UserRegisterModel userRegisterModel)
        {
            var isUserExists = await _context.Users.AnyAsync(e => e.Email == userRegisterModel.Email);
            if (isUserExists)
            {
                throw new ElementAlreadyExistsException("Пользователь с данным E-mail уже существует");
            }
        }

        public async Task<TokenResponse> Login(LoginCredentials loginCredentials)
        {
            var user = await _context.Users
                .Where(u => u.Email == loginCredentials.Email)
                .SingleOrDefaultAsync();

            var checkPassword = PasswordVerificationResult.Failed;
            if (user != null)
            {
                var hasher = new PasswordHasher<User>();
                checkPassword = hasher.VerifyHashedPassword(user, user.Password, loginCredentials.Password);
            }
            if (checkPassword == PasswordVerificationResult.Failed || user == null)
            {
                throw new IncorrectDataException("Неверный логин или пароль");
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

        private static string CreateToken(Guid id)
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
