using CSBUnlimited.Utils.Dsv.Exceptions;
using System;

namespace CSBUnlimited.Utils.Dsv.Attributes
{
    /// <summary>
    /// Attribute can use for define meta data for the dsv classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DsvMetaDataAttribute : Attribute
    {
        /// <summary>
        /// Index where this attribute defined property or field
        /// </summary>
        public int Index { get; set; } = -1;
        /// <summary>
        /// Dsv file header name that need to mapped this property or field
        /// </summary>
        public string HeaderName { get; set; } = null;
        /// <summary>
        /// Ignore this property or field
        /// </summary>
        public bool IsIgnore { get; set; } = false;
        /// <summary>
        /// Use this property or field for read only not for writing
        /// </summary>
        public bool IsOnlyForRead { get; set; } = false;

        /// <summary>
        /// Constructor for define only the index
        /// </summary>
        /// <param name="index">Index for this property or field</param>
        public DsvMetaDataAttribute(int index)
        {
            if (index < 0)
            {
                throw new InvalidDsvMetaDataException($"Invalid index value provided `{index}`. Index should greater than or equal to zero");
            }

            Index = index;
        }

        /// <summary>
        /// Constructor for define only the header name
        /// </summary>
        /// <param name="headerName">Header name for this property or field</param>
        public DsvMetaDataAttribute(string headerName)
        {
            if (string.IsNullOrWhiteSpace(headerName))
            {
                throw new InvalidDsvMetaDataException($"Header name cannot be empty");
            }

            HeaderName = headerName;
        }

        /// <summary>
        /// Constructor for define to ignore 
        /// </summary>
        /// <param name="isIgnore">Header name for this property or field</param>
        public DsvMetaDataAttribute(bool isIgnore)
        {
            IsIgnore = isIgnore;
        }

        /// <summary>
        /// Default constructor, but you must define Index or HeaderName or IsIgnore
        /// </summary>
        public DsvMetaDataAttribute()
        {
        }
    }
}
