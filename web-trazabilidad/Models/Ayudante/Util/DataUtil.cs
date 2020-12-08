using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Ayudante.Util
{
    public static class DataUtil
    {
        public static List<string> GetPropertiesName(this object property)
        {
            var propertiesClass = property.GetType().GetProperties();
            List<string> properties = new List<string>();

            foreach (var propertyClass in propertiesClass)
            {
                var attributes = Attribute.GetCustomAttributes(propertyClass).ToList();

                if (!attributes.Any(x => x.GetType() == typeof(IgnoreAttribute) || x.GetType() == typeof(DataSimpleAttribute) || x.GetType() == typeof(DataListAttribute)))
                {
                    Attribute nameAttribute = attributes.Where(x => x.GetType() == typeof(NameAttribute)).FirstOrDefault();
                    string name = nameAttribute == null ? null : ((NameAttribute)nameAttribute).Name;
                    properties.Add(name);
                }
            }

            return properties;
        }

        public static List<PropertyData> GetPropertiesData(this object property)
        {
            var propertiesClass = property.GetType().GetProperties();
            List<PropertyData> properties = new List<PropertyData>();

            foreach (var propertyClass in propertiesClass)
            {
                var attributes = Attribute.GetCustomAttributes(propertyClass).ToList();

                if (!attributes.Any(x => x.GetType() == typeof(IgnoreAttribute)))
                {
                    properties.Add(new PropertyData()
                    {
                        Name = propertyClass.Name,
                        Value = propertyClass.GetValue(property),
                        Attributes = attributes
                    });
                }
            }

            return properties;
        }

        public class PropertyData
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public List<Attribute> Attributes { get; set; }
        }
    }
}