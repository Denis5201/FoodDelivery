using FoodDelivery.Models;
using FoodDelivery.Models.DTO;

namespace FoodDelivery.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly DatabaseContext _context;

        public UserProfileService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task GetProfile()
        {
            
        }

        public async Task ChangeProfile(UserEditModel editModel)
        {
            
        }
    }
}
