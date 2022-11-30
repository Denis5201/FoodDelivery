using FoodDelivery.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Services.Implementation
{
    public class BackgroundTokenCleaningService : BackgroundService
    {
        private readonly ILogger<BackgroundTokenCleaningService> _logger;
        private readonly IServiceProvider _services;

        public BackgroundTokenCleaningService(ILogger<BackgroundTokenCleaningService> logger,
            IServiceProvider service)
        {
            _logger = logger;
            _services = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TokenCleaningService running");

            await Cleaning(stoppingToken);
        }

        private async Task Cleaning(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("TokenCleaningService cleaning");

                using var scope = _services.CreateScope();
                var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                var now = DateTime.Now;

                var invalidTokens = await databaseContext.InvalidTokens
                    .Where(t => t.ExitTime.AddMinutes(JwtConfigs.Lifetime) < now)
                    .ToListAsync(cancellationToken: stoppingToken);

                if (invalidTokens.Any())
                {
                    databaseContext.InvalidTokens.RemoveRange(invalidTokens);

                    databaseContext.SaveChanges();
                }

                await Task.Delay(TimeSpan.FromMinutes(JwtConfigs.Lifetime), stoppingToken);

            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TokenCleaningService stopping");

            await base.StopAsync(cancellationToken);
        }
    }
}
