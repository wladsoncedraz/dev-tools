using Newtonsoft.Json;

namespace tools.JSON;
public class JsonToolsNewtonSoft
{
    public static async Task<string> ConvertObjectToJSON(object objectSerializable)
    {
        if (objectSerializable == null)
            throw new ArgumentNullException(nameof(objectSerializable));

        string objectJSON = "";

        try
        {
            objectJSON = JsonConvert.SerializeObject(objectSerializable);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error trying to convert object to JSON. Message: {ex.Message}");
        }

        return objectJSON;
    }

    public static async Task<T> ConvertJSONToObject<T>(string jsonObjectDeserializable)
    {
        if (string.IsNullOrEmpty(jsonObjectDeserializable))
            throw new ArgumentNullException(nameof(jsonObjectDeserializable));

        try
        {
            var objectDeserialized = JsonConvert.DeserializeObject<T>(jsonObjectDeserializable);

            return objectDeserialized;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error trying to convert JSON to object. Message: {ex.Message}");
        }
    }
}