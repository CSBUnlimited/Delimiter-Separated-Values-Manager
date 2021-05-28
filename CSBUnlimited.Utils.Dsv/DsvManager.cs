using CSBUnlimited.Utils.Dsv.Core;
using System.Collections.Generic;

namespace CSBUnlimited.Utils.Dsv
{
    /// <summary>
    /// Dsv Manager, which include Dsv writer and Dsv Reader
    /// </summary>
    /// <typeparam name="T">Type of the Dsv Manger</typeparam>
    public class DsvManager<T> : IDsvManager<T>, IDsvReader<T>, IDsvWriter<T> where T : class
    {
        private string _delimeter;
        private bool _isHeaderAvailable;
        private string _filePath;

        public DsvReader<T> _dsvReader;
        public DsvWriter<T> _dsvWriter;



        /// <summary>
        /// Dsv Reader
        /// </summary>
        public IDsvReader<T> Reader => _dsvReader;
        /// <summary>
        /// Dsv Writer
        /// </summary>
        public IDsvWriter<T> Writer => _dsvWriter;

        /// <summary>
        /// Headers
        /// </summary>
        public string[] Headers => _dsvReader.Headers;
        /// <summary>
        /// Data provided by Dsv reader
        /// </summary>
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


        /// <summary>
        /// Get data provied Dsv Reader
        /// </summary>
        /// <returns>List of data</returns>
        public IEnumerable<T> ReadData() => _dsvReader.ReadData();

        /// <summary>
        /// Referesh Dsv Reader data
        /// </summary>
        public void Refresh()
        {
            _dsvReader.Refresh();
        }

        /// <summary>
        /// Write data to the file using Dsv Writer
        /// </summary>
        /// <param name="record">Record that need write to the file</param>
        public void WriteData(T record)
        {
            _dsvWriter.WriteData(record);
        }
    }
}
