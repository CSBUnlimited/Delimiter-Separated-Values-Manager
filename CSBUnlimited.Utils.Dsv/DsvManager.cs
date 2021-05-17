using CSBUnlimited.Utils.Dsv.Core;
using System.Collections.Generic;

namespace CSBUnlimited.Utils.Dsv
{
    public class DsvManager<T> : IDsvManager<T>, IDsvReader<T>, IDsvWriter<T> where T : class
    {
        private string _delimeter;
        private bool _isHeaderAvailable;
        private string _filePath;

        public DsvReader<T> _dsvReader;
        public DsvWriter<T> _dsvWriter;

        public IDsvReader<T> Reader => _dsvReader;
        public IDsvWriter<T> Writer => _dsvWriter;

        public string[] Headers => _dsvReader.Headers;
        public IEnumerable<T> Data => _dsvReader.Data;

        public DsvManager(bool isHeaderAvailable, string delimeter, string filePath)
        {
            _isHeaderAvailable = isHeaderAvailable;
            _delimeter = delimeter;
            _filePath = filePath;

            _dsvReader = new DsvReader<T>(_isHeaderAvailable, _delimeter, _filePath);
            _dsvWriter = new DsvWriter<T>(_isHeaderAvailable, _delimeter, _filePath, _dsvReader.Headers, _dsvReader);
        }

        public DsvManager(bool isHeaderAvailable, string delimeter, string filePath, string[] overrideHeaders)
        {
            _isHeaderAvailable = isHeaderAvailable;
            _delimeter = delimeter;
            _filePath = filePath;

            _dsvReader = new DsvReader<T>(_isHeaderAvailable, _delimeter, _filePath, overrideHeaders);
            _dsvWriter = new DsvWriter<T>(_isHeaderAvailable, _delimeter, _filePath, overrideHeaders, _dsvReader);
        }


        public IEnumerable<T> ReadData() => _dsvReader.ReadData();

        public void Refresh()
        {
            _dsvReader.Refresh();
        }

        public void WriteData(T record)
        {
            _dsvWriter.WriteData(record);
        }
    }
}
