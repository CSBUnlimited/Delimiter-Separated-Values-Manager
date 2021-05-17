using CSBUnlimited.Utils.Dsv.Core;

namespace CSBUnlimited.Utils.Dsv
{
    public class DsvWriter<T> : IDsvWriter<T> where T : class
    {
        private string _delimeter;
        private bool _isHeaderAvailable;
        private string _filePath;

        private IDsvReader<T> _dsvReader;

        public DsvWriter(bool isHeaderAvailable, string delimeter, string filePath)
        {
            _isHeaderAvailable = isHeaderAvailable;
            _delimeter = delimeter;
            _filePath = filePath;
            _dsvReader = null;
        }

        public DsvWriter(bool isHeaderAvailable, string delimeter, string filePath, IDsvReader<T> dsvReader) : this(isHeaderAvailable, delimeter, filePath)
        {
            _dsvReader = dsvReader;
        }

        
    }
}
