using CSBUnlimited.Utils.Dsv.Attributes;
using CSBUnlimited.Utils.Dsv.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CSBUnlimited.Utils.Dsv
{
    public static class DsvConverter
    {
        /// <summary>
        /// Deafult contructor should included in the model which tring to convert
        /// </summary>
        /// <typeparam name="T">Model Type</typeparam>
        /// <param name="dataLine"></param>
        /// <param name="headerLine"></param>
        /// <returns></returns>
        public static T DsvToModel<T>(string[] dataLine, string[] headerLine = null) where T : class
        {
            T model = (T)Activator.CreateInstance(typeof(T));
            return DsvToModel(model, dataLine, headerLine);
        }

        public static T DsvToModel<T>(T model, string[] dataLine, string[] headerLine = null) where T : class
        {
            Type type = model.GetType();
            PropertyInfo[] props = type.GetProperties();

            List<DsvDataRecordModel> dataRecordModels = new List<DsvDataRecordModel>();

            foreach(var data in dataLine.Select((value, index) => (value, index)))
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
    }
}
