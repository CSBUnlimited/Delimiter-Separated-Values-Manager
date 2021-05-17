using CSBUnlimited.Utils.Dsv.Core;
using System;

namespace CSBUnlimited.Utils.Dsv.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IDsvManager<CurrencyVM> manager = new DsvManager<CurrencyVM>(isHeaderAvailable: true, delimeter: ",", filePath: "Currencies.csv");
            manager.Initialize();
        }
    }
}
