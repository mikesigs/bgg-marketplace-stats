namespace BGGMarketPlaceStats.Core.Model;

public readonly record struct MarketplaceStatistics(
    string Condition,
    int NumberOfListings,
    Price AveragePrice,
    Price MinPrice,
    Price MaxPrice,
    Price MedianPrice,
    string? ModePrice
);