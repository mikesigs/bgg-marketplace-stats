using BGGMarketPlaceStats.Core.Model;

namespace BGGMarketPlaceStats.Core.Interfaces;

public interface IMarketplaceStatisticsService
{
    Task<ICollection<MarketplaceStatistics>> GetMarketplaceStatistics(Game game, Currency targetCurrency);
}