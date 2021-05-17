using System.Collections.Generic;

namespace CSBUnlimited.Utils.Dsv.Core
{
    public interface IDsvReader<T> where T : class
    {
        string[] Headers { get; }
        IEnumerable<T> Data { get; }

        void Refresh();
        IEnumerable<T> ReadData();
        string[] ReadHeaders();
    }
}
