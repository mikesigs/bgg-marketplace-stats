using BGGMarketPlaceStats.Core.Interfaces;
using BGGMarketPlaceStats.Core.Model;

namespace BGGMarketPlaceStats.Core.Services
{
    internal class MarketplaceStatisticsService(IExchangeRateService exchangeRateService) : IMarketplaceStatisticsService
    {
        public async Task<ICollection<MarketplaceStatistics>> GetMarketplaceStatistics(Game game, Currency targetCurrency)
        {
            var listings = await GetListingsInTargetCurrency(game, targetCurrency);
            return CalculateStatistics(listings).ToList();
        }

        private async Task<ICollection<MarketplaceListing>> GetListingsInTargetCurrency(Game game, Currency targetCurrency)
        {
            var listings = new List<MarketplaceListing>();

            foreach (var listing in game.MarketplaceListings)
            {
                if (listing.Price.Currency == targetCurrency)
                {
                    listings.Add(listing);
                }
                else
                {
                    var rate = await exchangeRateService.GetExchangeRate(from: listing.Price.Currency, to: targetCurrency);
                    listings.Add(listing with
                    {
                        Price = listing.Price.ChangeCurrency(targetCurrency, rate)
                    });
                }
            }

            return listings;
        }

        private IEnumerable<MarketplaceStatistics> CalculateStatistics(ICollection<MarketplaceListing> listings)
        {
            var prices = listings.Select(l => l.Price).ToList();

            yield return new MarketplaceStatistics
            {
                Condition = "any",
                NumberOfListings = listings.Count,
                AveragePrice = GetAverage(prices),
                MinPrice = GetMinimum(prices),
                MaxPrice = GetMaximum(prices),
                MedianPrice = GetMedian(prices),
                ModePrice = GetMode(prices)
            };

            foreach (var conditionGroup in listings.GroupBy(l => l.Condition))
            {
                var pricesByCondition = conditionGroup.Select(l => l.Price).ToList();
                yield return new MarketplaceStatistics
                {
                    Condition = conditionGroup.Key,
                    NumberOfListings = pricesByCondition.Count,
                    AveragePrice = GetAverage(pricesByCondition),
                    MinPrice = GetMinimum(pricesByCondition),
                    MaxPrice = GetMaximum(pricesByCondition),
                    MedianPrice = GetMedian(pricesByCondition),
                    ModePrice = GetMode(pricesByCondition)
                };
            }
        }

        private static Price GetAverage(List<Price> prices)
        {
            var average = prices.Select(p => p.Value).Average();
            return new Price(average, prices.First().Currency);
        }

        private static Price GetMinimum(List<Price> prices)
        {
            return prices.Min();
        }

        private static Price GetMaximum(List<Price> prices)
        {
            return prices.Max();
        }

        private static Price GetMedian(List<Price> prices)
        {
            var sortedPrices = prices.OrderBy(p => p.Value).ToList();
            var middle = sortedPrices.Count / 2;
            if (sortedPrices.Count % 2 == 0)
            {
                return (sortedPrices[middle - 1] + sortedPrices[middle]) / 2.0m;
            }
            return sortedPrices[middle];
        }

        private static string? GetMode(List<Price> prices)
        {
            var groupedPrices = prices.GroupBy(p => Math.Round(p.Value, 0)).OrderByDescending(g => g.Count()).ToList();
            var frequency = groupedPrices.First().Count();
            var mode = groupedPrices.Where(g => g.Count() == frequency).Select(g => g.Key).ToList();

            if (mode.Count == groupedPrices.Count)
            {
                return null;
            }

            var totalFrequency = frequency * mode.Count;
            var pct = Math.Floor((decimal)totalFrequency / prices.Count * 100);
            return $"{string.Join(", ", mode)} ({totalFrequency} / {prices.Count} listings, {pct}%)";
        }
    }
}
