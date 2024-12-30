using BGGMarketPlaceStats.Core.Model;

namespace BGGMarketPlaceStats.Core.Interfaces;

public interface IBggService
{
    Task<Game> GetGame(int gameId);
}