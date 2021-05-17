using CSBUnlimited.Utils.Dsv.Core;
using System.Collections.Generic;

namespace CSBUnlimited.Utils.Dsv
{
    public class DsvManager<T> : IDsvManager<T>, IDsvReader<T> where T : class
    {
        private string _delimeter;
        private bool _isHeaderAvailable;
        private string _filePath;

        public IDsvReader<T> DsvReader { get; }

        public string[] Headers => DsvReader.Headers;
        public IEnumerable<T> Data => DsvReader.Data;

        public DsvManager(bool isHeaderAvailable, string delimeter, string filePath)
        {
            _isHeaderAvailable = isHeaderAvailable;
            _delimeter = delimeter;
            _filePath = filePath;

            DsvReader = new DsvReader<T>(_isHeaderAvailable, _delimeter, _filePath);
        }

        public void Refresh()
        {
            DsvReader.Refresh();
        }

        public IEnumerable<T> ReadData() => DsvReader.ReadData();
        public string[] ReadHeaders() => DsvReader.ReadHeaders();
    }
}
