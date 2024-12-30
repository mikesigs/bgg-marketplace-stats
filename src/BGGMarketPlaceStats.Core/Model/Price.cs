namespace BGGMarketPlaceStats.Core.Model
{
    public readonly record struct Price(decimal Value, Currency Currency) : IComparable<Price>
    {
        public Price ChangeCurrency(Currency to, decimal conversionRate)
        {
            return new Price(Value * conversionRate, to);
        }

        public static Price operator +(Price a, Price b)
        {
            if (a.Currency != b.Currency)
            {
                throw new InvalidOperationException("Cannot add prices with different currencies");
            }
            return a with
            {
                Value = a.Value + b.Value
            };
        }

        public static Price operator /(Price a, decimal b)
        {
            return a with
            {
                Value = a.Value / b
            };
        }

        public int CompareTo(Price other)
        {
            if (Currency != other.Currency)
            {
                throw new InvalidOperationException("Cannot compare prices with different currencies");
            }
            return Value.CompareTo(other.Value);
        }
    }
}
