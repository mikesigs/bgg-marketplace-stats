using Ardalis.SmartEnum;

namespace BGGMarketPlaceStats.Core.Model;

public class Currency : SmartEnum<Currency>
{
    public static readonly Currency CAD = new(nameof(CAD), 1);
    public static readonly Currency USD = new(nameof(USD), 2);
    public static readonly Currency EUR = new(nameof(EUR), 3);
    public static readonly Currency GBP = new(nameof(GBP), 4);
    public static readonly Currency AUD = new(nameof(AUD), 5);
    /*Australian dollar
       Brazilian real
       Chinese renminbi
       European euro
       Hong Kong dollar
       Indian rupee
       Indonesian rupiah
       Japanese yen
       Mexican peso
       New Zealand dollar
       Norwegian krone
       Peruvian new sol
       Russian ruble
       Saudi riyal
       Singapore dollar
       South African rand
       South Korean won
       Swedish krona
       Swiss franc
       Taiwanese dollar
       Turkish lira
       UK pound sterling
       US dollar*/
    public static bool IsValid(string name) => TryFromName(name, out _);

    private Currency(string name, int value) : base(name, value)
    {
    }
}