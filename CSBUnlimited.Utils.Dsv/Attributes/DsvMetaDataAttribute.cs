using CSBUnlimited.Utils.Dsv.Exceptions;
using System;

namespace CSBUnlimited.Utils.Dsv.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DsvMetaDataAttribute : Attribute
    {
        public int Index { get; set; } = -1;
        public string HeaderName { get; set; } = null;
        public bool IsIgnore { get; set; } = false;
        public bool IsOnlyForRead { get; set; } = false;

        public DsvMetaDataAttribute(int index)
        {
            if (index < 0)
            {
                throw new InvalidDsvMetaDataException($"Invalid index value provided `{index}`. Index should greater than or equal to zero");
            }

            Index = index;
        }

        public DsvMetaDataAttribute(string headerName)
        {
            if (string.IsNullOrWhiteSpace(headerName))
            {
                throw new InvalidDsvMetaDataException($"Header name cannot be empty");
            }

            HeaderName = headerName;
        }

        public DsvMetaDataAttribute(bool isIgnore)
        {
            IsIgnore = isIgnore;
        }

        public DsvMetaDataAttribute()
        {
        }
    }
}
