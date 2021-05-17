using System.Collections.Generic;

namespace CSBUnlimited.Utils.Dsv.Core
{
    public interface IDsvManager<T> where T : class
    {
        string[] Headers { get; }
        IEnumerable<T> Data { get; }

        void Refresh();
    }
}
