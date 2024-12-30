using BGGMarketPlaceStats.Core.Model;

namespace BGGMarketPlaceStats.Core.Interfaces;

public interface IExchangeRateService
{
    Task<decimal> GetExchangeRate(Currency from, Currency to);
}