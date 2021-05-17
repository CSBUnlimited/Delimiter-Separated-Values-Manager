using CSBUnlimited.Utils.Dsv.Attributes;
using System;

namespace CSBUnlimited.Utils.Dsv.Test
{
    public class CurrencyVM
    {
        public string CurrencyName { get; set; }
        [DsvMetaData("NumCode")]
        public string NumericCode { get; set; }
        [DsvMetaData(HeaderName = "NumCode")]
        public string AlphaCode { get; set; }
        [DsvMetaData(3)]
        public string ArabicName { get; set; }

        [DsvMetaData(Index = 1)]
        public int NumericCodeInt { get; set; }

        [DsvMetaData(IsIgnore = false)]
        public DateTime CurrentDateTime { get; set; }
    }
}
