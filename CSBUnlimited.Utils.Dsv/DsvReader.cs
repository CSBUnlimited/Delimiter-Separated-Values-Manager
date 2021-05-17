using CSBUnlimited.Utils.Dsv.Common;
using CSBUnlimited.Utils.Dsv.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSBUnlimited.Utils.Dsv
{
    public class DsvReader<T> : IDsvReader<T> where T : class
    {
        private string _delimeter;
        private bool _isHeaderAvailableInFile;
        private string _filePath;
        private List<T> _data;

        public string[] Headers { get; protected set; } = null;
        public IEnumerable<T> Data => _data;

        public DsvReader(bool isHeaderAvailable, string delimeter, string filePath)
        {
            _isHeaderAvailableInFile = isHeaderAvailable;
            _delimeter = delimeter;
            _filePath = filePath;

            Refresh();
        }

        public DsvReader(bool isHeaderAvailableInFile, string delimeter, string filePath, string[] overrideHeaders)
        {
            _isHeaderAvailableInFile = isHeaderAvailableInFile;
            _delimeter = delimeter;
            _filePath = filePath;
            Headers = overrideHeaders;

            Refresh();
        }

        public IEnumerable<T> ReadData() => _data;
        public string[] ReadHeaders() => Headers;

        public void Refresh()
        {
            if (CommonMethods.IsFileExistsAndNotEmpty(_filePath))
            {
                using (var reader = new StreamReader(_filePath))
                {
                    List<T> data = new List<T>();

                    int lineNumber = 0;
                    while (!reader.EndOfStream)
                    {
                        lineNumber++;
                        string line = reader.ReadLine();
                        string[] values = line.Split(new string[] { _delimeter }, StringSplitOptions.None);

                        if (lineNumber == 1 && _isHeaderAvailableInFile)
                        {
                            if (Headers == null)
                            {
                                Headers = values;
                            }
                        }
                        else
                        {
                            data.Add(DsvConverter.StringArrayToModel<T>(values, Headers));
                        }
                    }

                    _data = data;
                    reader.Close();
                }
            }
            else
            {
                _data = new List<T>();
            }
        }

        internal void AddDataRecord(string[] dataLine)
        {
            _data.Add(DsvConverter.StringArrayToModel<T>(dataLine, Headers));
        }
    }
}
