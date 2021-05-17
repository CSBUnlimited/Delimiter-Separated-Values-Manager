using CSBUnlimited.Utils.Dsv.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSBUnlimited.Utils.Dsv
{
    public class DsvReader<T> : IDsvReader<T> where T : class
    {
        private string _delimeter;
        private bool _isHeaderAvailable;
        private string _filePath;

        public string[] Headers { get; protected set; }
        public IEnumerable<T> Data { get; protected set; }

        public DsvReader(bool isHeaderAvailable, string delimeter, string filePath)
        {
            _isHeaderAvailable = isHeaderAvailable;
            _delimeter = delimeter;
            _filePath = filePath;

            Refresh();
        }

        public void Refresh()
        {
            using (var reader = new StreamReader(_filePath))
            {
                List<T> data = new List<T>();

                int lineNumber = 0;
                while(!reader.EndOfStream)
                {
                    lineNumber++;
                    string line = reader.ReadLine();
                    string[] values = line.Split(new string[] { _delimeter }, StringSplitOptions.None);

                    if (lineNumber == 1 && _isHeaderAvailable)
                    {
                        Headers = values;
                    }
                    else
                    {
                        data.Add(DsvConverter.DsvToModel<T>(values, Headers));
                    }
                }

                Data = data;
                reader.Close();
            }
        }

        public IEnumerable<T> ReadData() => Data;
        public string[] ReadHeaders() => Headers;
    }
}
