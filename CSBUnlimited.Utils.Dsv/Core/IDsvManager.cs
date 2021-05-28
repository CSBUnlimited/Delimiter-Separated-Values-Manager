using System.Collections.Generic;

namespace CSBUnlimited.Utils.Dsv.Core
{
    /// <summary>
    /// Dsv Manager, which include Dsv writer and Dsv Reader
    /// </summary>
    /// <typeparam name="T">Type of the Dsv Manger</typeparam>
    public interface IDsvManager<T> where T : class
    {
        /// <summary>
        /// Headers
        /// </summary>
        string[] Headers { get; }
        /// <summary>
        /// Data provided by Dsv reader
        /// </summary>
        IEnumerable<T> Data { get; }
        /// <summary>
        /// Dsv Reader
        /// </summary>
        IDsvReader<T> Reader { get; }
        /// <summary>
        /// Dsv Writer
        /// </summary>
        IDsvWriter<T> Writer { get; }

        /// <summary>
        /// Referesh Dsv Reader data
        /// </summary>
        void Refresh();
        /// <summary>
        /// Get data provied Dsv Reader
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> ReadData();
        /// <summary>
        /// Write data to the file using Dsv Writer
        /// </summary>
        /// <param name="record">Record that need write to the file</param>
        void WriteData(T record);
    }
}
