using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using FoodDelivery.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services.Implementation
{
    public class UserProfileService : IUserProfileService
    {
        private readonly DatabaseContext _context;

        public UserProfileService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<UserDto> GetProfile(string id)
        {
            var profile = await _context.Users
                .Where(i => i.Id == Guid.Parse(id))
                .Select(p => new UserDto
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    BirthDate = p.BirthDate,
                    Gender = p.Gender,
                    Address = p.Address,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                }).SingleAsync();
            return profile;
        }

        public async Task ChangeProfile(UserEditModel editModel, string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            if (user == null)
            {
                return;
            }
            user.FullName = editModel.FullName;
            user.BirthDate = editModel.BirthDate;
            user.Gender = editModel.Gender;
            user.Address = editModel.Address;
            user.PhoneNumber = editModel.PhoneNumber;

            await _context.SaveChangesAsync();
        }
    }
}
