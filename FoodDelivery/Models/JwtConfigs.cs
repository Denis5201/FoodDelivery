using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FoodDelivery.Models
{
    public class JwtConfigs
    {
        public const string Issuer = "JwtIssuer";
        public const string Audience = "JwtClient";
        private const string Key = "SuperSecretKey!*578457";
        public const int Lifetime = 30; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

    }
}
