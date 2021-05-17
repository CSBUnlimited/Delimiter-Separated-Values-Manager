using CSBUnlimited.Utils.Dsv.Common;
using CSBUnlimited.Utils.Dsv.Core;
using System.IO;

namespace CSBUnlimited.Utils.Dsv
{
    public class DsvWriter<T> : IDsvWriter<T> where T : class
    {
        private readonly string _delimeter;
        private readonly bool _isHeaderAvailable;
        private readonly string _filePath;
        private readonly string[] _headers;

        private DsvReader<T> _dsvReader;

        public DsvWriter(bool isHeaderAvailable, string delimeter, string filePath, string[] headers = null)
        {
            _isHeaderAvailable = isHeaderAvailable;
            _delimeter = delimeter;
            _filePath = filePath;
            _headers = headers;
            _dsvReader = null;
        }

        internal DsvWriter(bool isHeaderAvailable, string delimeter, string filePath, string[] headers, DsvReader<T> dsvReader) : this(isHeaderAvailable, delimeter, filePath, headers)
        {
            _dsvReader = dsvReader;
        }

        public void WriteData(T record)
        {
            string[] dataArray;
            bool isWriteHeader = false;

            if (!CommonMethods.IsFileExistsAndNotEmpty(_filePath) && _isHeaderAvailable && _headers != null)
            {
                isWriteHeader = true;
            }

            using (var reader = new StreamWriter(_filePath, true))
            {
                if (isWriteHeader)
                {
                    reader.WriteLine(string.Join(_delimeter, _headers));
                }

                dataArray = DsvConverter.ModelToStringArray(record, _headers);

                reader.WriteLine(string.Join(_delimeter, dataArray));

                reader.Close();
            }

            _dsvReader?.AddDataRecord(dataArray);
        }
    }
}
