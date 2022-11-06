using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace tools.XML
{
    public class XMLNative
    {
        public static async Task<string> ConvertObjectToXML(object objectSerializable)
        {
            if (objectSerializable == null)
                throw new ArgumentNullException(nameof(objectSerializable));

            StringWriter writer = new StringWriter();

            try
            {
                XmlSerializer serializer = new XmlSerializer(objectSerializable.GetType());

                serializer.Serialize(writer, objectSerializable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error trying convert object to XML. Message: {ex.Message}");
            }

            return writer.ToString();
        }


        public static async Task<T> ConvertXMLToObject<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException(nameof(xml));

            StringReader reader = new StringReader(xml);
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            try
            {
                var objectDeserialized = serializer.Deserialize(reader);

                return (T)objectDeserialized;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error trying convert object to XML. Message: {ex.Message}");
            }
        }
    }
}