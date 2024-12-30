namespace BGGMarketPlaceStats.Infrastructure.ExchangeRates
{
    public class ExchangeRateServiceConfiguration
    {
        public const string SectionName = "ExchangeRateService";

        public Uri? BaseAddress { get; set; }
    }
}
