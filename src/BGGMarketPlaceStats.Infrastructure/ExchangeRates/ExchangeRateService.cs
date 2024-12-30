using BGGMarketPlaceStats.Core.Interfaces;
using BGGMarketPlaceStats.Core.Model;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace BGGMarketPlaceStats.Infrastructure.ExchangeRates;

public class ExchangeRateService(HttpClient http, IMemoryCache cache) : IExchangeRateService
{
    private static readonly TimeSpan TwoHours = TimeSpan.FromHours(2);

    public async Task<decimal> GetExchangeRate(Currency from, Currency to)
    {
        var cacheKey = $"{from.Name}_{to.Name}";
        if (cache.TryGetValue(cacheKey, out decimal exchangeRate))
        {
            return exchangeRate;
        }

        var exchangeRateResponse = await http.GetFromJsonAsync<ExchangeRateResponse>($"/v6/latest/{from.Name}");
        exchangeRate = exchangeRateResponse!.Rates[to.Name];

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(DateTimeOffset.Parse(exchangeRateResponse.TimeNextUpdateUtc));

        cache.Set(cacheKey, exchangeRate, cacheEntryOptions);

        return exchangeRate;
    }

    public class ExchangeRateResponse
    {
        [JsonPropertyName("result")]
        public required string Result { get; set; }

        [JsonPropertyName("provider")]
        public required string Provider { get; set; }

        [JsonPropertyName("documentation")]
        public required string Documentation { get; set; }

        [JsonPropertyName("terms_of_use")]
        public required string TermsOfUse { get; set; }

        [JsonPropertyName("time_last_update_unix")]
        public long TimeLastUpdateUnix { get; set; }

        [JsonPropertyName("time_last_update_utc")]
        public required string TimeLastUpdateUtc { get; set; }

        [JsonPropertyName("time_next_update_unix")]
        public long TimeNextUpdateUnix { get; set; }

        [JsonPropertyName("time_next_update_utc")]
        public required string TimeNextUpdateUtc { get; set; }

        [JsonPropertyName("time_eol_unix")]
        public long TimeEolUnix { get; set; }

        [JsonPropertyName("base_code")]
        public required string BaseCode { get; set; }

        [JsonPropertyName("rates")]
        public required Dictionary<string, decimal> Rates { get; set; }
    }

}
