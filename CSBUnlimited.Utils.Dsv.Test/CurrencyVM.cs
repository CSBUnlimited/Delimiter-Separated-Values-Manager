using CSBUnlimited.Utils.Dsv.Attributes;
using System;

namespace CSBUnlimited.Utils.Dsv.Test
{
    public class CurrencyVM
    {
        [DsvMetaData(0)]
        public string CurrencyName { get; set; }
        [DsvMetaData("NumCode")]
        public string NumericCode { get; set; }
        [DsvMetaData(HeaderName = "AlphaCode")]
        public string AlphaCode { get; set; }
        [DsvMetaData(3)]
        public string ArabicName { get; set; }

        [DsvMetaData(Index = 1)]
        public int NumericCodeInt { get; set; }

        [DsvMetaData(IsIgnore = false)]
        public DateTime CurrentDateTime { get; set; }
    }
}
