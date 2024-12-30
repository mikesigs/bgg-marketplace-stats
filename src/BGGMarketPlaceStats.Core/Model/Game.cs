using System.Collections.Immutable;

namespace BGGMarketPlaceStats.Core.Model;

public record Game
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required ImmutableList<MarketplaceListing> MarketplaceListings { get; init; }
}