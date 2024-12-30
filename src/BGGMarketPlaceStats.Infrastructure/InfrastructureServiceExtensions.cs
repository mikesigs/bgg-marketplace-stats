using BGGMarketPlaceStats.Core.Interfaces;
using BGGMarketPlaceStats.Infrastructure.BGG;
using BGGMarketPlaceStats.Infrastructure.ExchangeRates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BGGMarketPlaceStats.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddMemoryCache();

        services.Configure<ExchangeRateServiceConfiguration>(configuration.GetSection(ExchangeRateServiceConfiguration.SectionName));
        services.AddHttpClient<IExchangeRateService, ExchangeRateService>((provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<ExchangeRateServiceConfiguration>>().Value;
            client.BaseAddress = config.BaseAddress;
        });

        services.Configure<BggServiceConfiguration>(configuration.GetSection(BggServiceConfiguration.SectionName));
        services.AddHttpClient<IBggService, BggService>((provider, client) =>
        {
            var config = provider.GetRequiredService<IOptions<BggServiceConfiguration>>().Value;
            client.BaseAddress = config.BaseAddress;
        });

        return services;
    }
}