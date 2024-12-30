using BGGMarketPlaceStats.Core.Interfaces;
using BGGMarketPlaceStats.Core.Model;
using System.Collections.Immutable;
using System.Xml.Serialization;

namespace BGGMarketPlaceStats.Infrastructure.BGG
{
    public class BggService(HttpClient http) : IBggService
    {
        public async Task<Game> GetGame(int gameId)
        {
            var responseContent = await http.GetStringAsync($"/xmlapi2/thing?type=boardgame&id={gameId}&marketplace=1");
            var items = DeserializeResponse(responseContent);
            var bggItem = items.ItemList.First();

            return new Game
            {
                Id = bggItem.Id,
                Name = bggItem.Names.First(n => n.Type == "primary").Value,
                MarketplaceListings = bggItem.MarketplaceListings.Listings
                    .Where(l => Currency.IsValid(l.Price.Currency))
                    .Select(l => new MarketplaceListing
                    {
                        Condition = l.Condition.Value,
                        Price = new Price(l.Price.Value, Currency.FromName(l.Price.Currency))
                    })
                    .ToImmutableList()
            };
        }

        private static BggXmlApi2Response.Items DeserializeResponse(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(BggXmlApi2Response.Items));
            using var reader = new StringReader(xmlString);
            var items = (BggXmlApi2Response.Items?)serializer.Deserialize(reader);
            return items ?? throw new Exception("Failed to deserialize BGG response");
        }
    }
}