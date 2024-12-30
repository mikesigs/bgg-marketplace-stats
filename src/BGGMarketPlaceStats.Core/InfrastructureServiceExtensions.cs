using BGGMarketPlaceStats.Core.Interfaces;
using BGGMarketPlaceStats.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BGGMarketPlaceStats.Core;

public static class CoreServiceExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services,
        ConfigurationManager config)
    {
        services.AddTransient<IMarketplaceStatisticsService, MarketplaceStatisticsService>();

        return services;
    }
}