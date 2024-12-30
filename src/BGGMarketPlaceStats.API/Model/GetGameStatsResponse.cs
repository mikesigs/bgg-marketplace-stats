using BGGMarketPlaceStats.Core.Model;

namespace BGGMarketPlaceStats.API.Model;

public readonly record struct GetGameStatsResponse(int GameId, string GameName, string Condition, int Count, decimal Minimum, decimal Maximum, decimal Average, decimal Median, string? Mode);
public readonly record struct GetGameStatsResponse2(int GameId, string GameName, ICollection<MarketplaceStatistics> Statistics);