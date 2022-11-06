using System.Text.Json;
using System.Text.Json.Serialization;

namespace tools.JSON;
public class JsonToolsNative
{
    public async Task<string> ConvertObjectToJSON(object objectSerializable)
    {
        if (objectSerializable == null)
            throw new ArgumentNullException(nameof(objectSerializable));

        string objectJSON = "";

        // Options to configure Json output 
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        try
        {
            objectJSON = JsonSerializer.Serialize(objectSerializable, options);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error trying to convert object to JSON. Message: {ex.Message}");
        }

        return objectJSON;
    }
    public async Task<T> ConvertJSONToObject<T>(string jsonObjectDeserializable)
    {
        if (string.IsNullOrEmpty(jsonObjectDeserializable))
            throw new ArgumentNullException(nameof(jsonObjectDeserializable));

        try
        {
            var objectDeserialized = JsonSerializer.Deserialize<T>(jsonObjectDeserializable);

            return objectDeserialized;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error trying to convert JSON to object. Message: {ex.Message}");
        }
    }

    public async Task ProcessJSON(string json)
    {
        if (string.IsNullOrEmpty(json))
            throw new ArgumentNullException(nameof(json));

        json = @"{""Nome"":""Carlos Silva"",""Idade"":33, ""Telefones"": { ""celular"": ""11-99999-9999"", ""comercial"": ""11-4444-4444""}}";

        try
        {
            var jsonDocument = JsonDocument.Parse(json);

            // Getting name and phone number into Json string 
            var name = jsonDocument.RootElement.GetProperty("Nome").GetString();
            var phone = jsonDocument.RootElement.GetProperty("Telefones").GetProperty("celular").ToString();

            // Getting only phones 'comercial' into Json string
            var phones = jsonDocument.RootElement.EnumerateObject()
                            .Where(jsonProperty => jsonProperty.Name.Contains("Telefones") && jsonProperty.Value.ValueKind == JsonValueKind.Object)
                            .Select(jsonProperty => jsonProperty.Value.GetProperty("comercial"));

        }
        catch (Exception ex)
        {
            throw new Exception($"Error trying process JSON. Message: {ex.Message}");
        }
    }
}