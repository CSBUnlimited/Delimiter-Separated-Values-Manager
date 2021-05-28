using CSBUnlimited.Utils.Dsv.Attributes;
using CSBUnlimited.Utils.Dsv.Exceptions;
using CSBUnlimited.Utils.Dsv.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CSBUnlimited.Utils.Dsv.Common
{
    internal static class DsvConverter
    {
        /// <summary>
        /// Deafult contructor should included in the model which tring to convert
        /// </summary>
        /// <typeparam name="T">Model Type</typeparam>
        /// <param name="dataLine"></param>
        /// <param name="headerLine"></param>
        /// <returns></returns>
        public static T StringArrayToModel<T>(string[] dataLine, string[] headerLine = null) where T : class
        {
            T model = (T)Activator.CreateInstance(typeof(T));
            return StringArrayToModel(model, dataLine, headerLine);
        }

        public static T StringArrayToModel<T>(T model, string[] dataLine, string[] headerLine = null) where T : class
        {
            Type type = model.GetType();
            PropertyInfo[] props = type.GetProperties();

            List<DsvDataRecordModel> dataRecordModels = new List<DsvDataRecordModel>();

            foreach (var data in dataLine.Select((value, index) => (value, index)))
            {
                DsvDataRecordModel recordModel = new DsvDataRecordModel()
                {
                    Index = data.index,
                    Value = data.value,
                    HeaderName = headerLine?.ElementAt(data.index)
                };

                dataRecordModels.Add(recordModel);
            }

            // Update props
            foreach (var prop in props)
            {
                if (!((prop.MemberType == MemberTypes.Property || prop.MemberType == MemberTypes.Field) && prop.CanWrite))
                {
                    continue;
                }

                DsvMetaDataAttribute attributeData = prop.GetCustomAttribute<DsvMetaDataAttribute>();

                if (attributeData == null)
                {
                    attributeData = new DsvMetaDataAttribute(prop.Name);
                }

                // If ignored then continue to next
                if (attributeData.IsIgnore)
                {
                    continue;
                }

                // Find data record
                DsvDataRecordModel dataRecord = null;

                // Find by index
                if (attributeData.Index >= 0)
                {
                    dataRecord = dataRecordModels.Find(drms => drms.Index == attributeData.Index);
                }
                //Find by header or property name
                else
                {
                    dataRecord = dataRecordModels.Find(drms => drms.HeaderName == attributeData.HeaderName);
                }

                // Set property values to the object
                if (dataRecord != null)
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(prop.PropertyType);

                    if (converter != null && converter.IsValid(dataRecord.Value))
                    {
                        prop.SetValue(model, converter.ConvertFromString(dataRecord.Value));
                    }
                }
            }

            return model;
        }

        public static string[] ModelToStringArray<T>(T model, string[] headerLine = null) where T : class
        {
            IList<string> headersList = headerLine?.ToList();

            Type type = model.GetType();
            PropertyInfo[] props = type.GetProperties();

            Dictionary<int, string> dataListDict = new Dictionary<int, string>();

            void AddIfNotExists(int key, string value)
            {
                if (!dataListDict.ContainsKey(key))
                {
                    dataListDict.Add(key, value);
                }
            }

            foreach (var prop in props)
            {
                if (!((prop.MemberType == MemberTypes.Property || prop.MemberType == MemberTypes.Field) && prop.CanRead))
                {
                    continue;
                }

                DsvMetaDataAttribute attributeData = prop.GetCustomAttribute<DsvMetaDataAttribute>();

                if (attributeData == null)
                {
                    attributeData = new DsvMetaDataAttribute(prop.Name);
                }

                // If ignored then continue to next
                if (attributeData.IsIgnore || attributeData.IsOnlyForRead)
                {
                    continue;
                }

                DsvDataRecordModel recordModel = new DsvDataRecordModel()
                {
                    Index = attributeData.Index,
                    Value = prop.GetValue(model)?.ToString(),
                    HeaderName = attributeData.HeaderName
                };

                if (headersList == null)
                {
                    if (attributeData.Index < 0)
                    {
                        throw new InvalidDsvMetaDataException($"No Dsv Meta Data Index provided or header line provided for `{prop.Name}`");
                    }

                    AddIfNotExists(recordModel.Index, recordModel.Value);
                }
                else
                {
                    if (attributeData.Index >= 0)
                    {
                        AddIfNotExists(recordModel.Index, recordModel.Value);
                    }
                    else
                    {
                        int index = headersList.IndexOf(recordModel.HeaderName);

                        if (index >= 0)
                        {
                            AddIfNotExists(index, recordModel.Value);
                        }
                    }
                }
            }

            if (dataListDict.Count == 0)
            {
                return null;
            }

            int length = headersList?.Count ?? dataListDict.Keys.Max() + 1;

            string[] dataList = new string[length];

            foreach (var data in dataListDict)
            {
                if (data.Key >= length)
                {
                    throw new InvalidDsvMetaDataException("Invalid meta data, data list exceeds the length of delimeter separated values");
                }

                dataList[data.Key] = data.Value;
            }

            return dataList;
        }
    }
}
