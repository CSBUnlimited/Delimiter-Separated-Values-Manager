using CSBUnlimited.Utils.Dsv.Core;

namespace CSBUnlimited.Utils.Dsv.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] headers = new string[] { "CurrencyName", "NumCode", "AlphaCode", "CurrencyName_Arb" };
            IDsvManager<CurrencyVM> manager = new DsvManager<CurrencyVM>(isHeaderAvailable: true, delimeter: ",", filePath: "Currencies.csv", overrideHeaders: headers);

            manager.WriteData(new CurrencyVM() { CurrencyName = "Sri Lankan Rupees", AlphaCode = "LKR2", NumericCode = "5556" });
        }
    }
}
