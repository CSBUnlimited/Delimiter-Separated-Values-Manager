namespace CSBUnlimited.Utils.Dsv.Core
{
    internal interface IDsvReaderAddRecord<T> where T : class
    {
        string[] Headers { get; }

        void AddDataRecord(T record);
    }
}
