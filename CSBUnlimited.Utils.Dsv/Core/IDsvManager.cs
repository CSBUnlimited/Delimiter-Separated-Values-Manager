using System.Collections.Generic;

namespace CSBUnlimited.Utils.Dsv.Core
{
    public interface IDsvManager<T> where T : class
    {
        string[] Headers { get; }
        IEnumerable<T> Data { get; }

        IDsvReader<T> Reader { get; }
        IDsvWriter<T> Writer { get; }

        void Refresh();
        IEnumerable<T> ReadData();
        void WriteData(T record);
    }
}
