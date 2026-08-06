using DiscordLite.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiscordLite.Infrastructure.BackgroundServices;

public sealed class RefreshTokenCleanupService(
    IServiceScopeFactory scopeFactory,
    ILogger<RefreshTokenCleanupService> logger) : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromHours(24);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IRefreshTokenRepository>();

            try
            {
                var removed = await repository.RemoveExpiredAsync(stoppingToken);
                logger.LogInformation("Removed {Count} expired refresh tokens.", removed);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to clean up expired refresh tokens.");
            }

            await Task.Delay(Interval, stoppingToken);
        }
    }
}