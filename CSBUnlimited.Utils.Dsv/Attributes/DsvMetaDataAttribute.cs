using System;

namespace CSBUnlimited.Utils.Dsv.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DsvMetaDataAttribute : Attribute
    {
        public int Index { get; set; } = -999;
        public string HeaderName { get; set; } = null;
        public bool IsIgnore { get; set; } = false;

        public DsvMetaDataAttribute(int index)
        {
            Index = index;
        }

        public DsvMetaDataAttribute(string headerName)
        {
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
