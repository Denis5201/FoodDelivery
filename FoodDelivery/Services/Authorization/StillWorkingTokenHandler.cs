using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace FoodDelivery.Services.Authorization
{
    public class StillWorkingTokenHandler : AuthorizationHandler<StillWorkingTokenRequirement>
    {
        private readonly IServiceProvider _services;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StillWorkingTokenHandler(IServiceProvider services, IHttpContextAccessor httpContextAccessor)
        {
            _services = services;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, StillWorkingTokenRequirement requirement)
        {
            string? token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization
                .ToString().Replace("Bearer ", "");

            if (!token.IsNullOrEmpty())
            {
                using var scope = _services.CreateScope();
                var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                var isNonWorking = databaseContext.InvalidTokens
                    .Where(t => t.Token == token)
                    .Any();

                if (!isNonWorking)
                {
                    context.Succeed(requirement);
                }
            }
            
            return Task.CompletedTask;
        }
    }
}
