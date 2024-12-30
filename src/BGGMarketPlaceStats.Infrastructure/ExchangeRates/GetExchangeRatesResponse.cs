namespace BGGMarketPlaceStats.Infrastructure.ExchangeRates;

internal class GetExchangeRatesResponse
{
    public required List<ExchangeRate> ForeignExchangeRates { get; init; }

    internal class ExchangeRate
    {
        public required Currency FromCurrency { get; set; }
        public required Currency ToCurrency { get; set; }
        public decimal Rate { get; set; }
    }

    internal class Currency
    {
        public required string Value { get; set; }
    }
}