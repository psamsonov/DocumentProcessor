using System.Text.Json;

public class Constants
{
    public const string API_URL = "http://localhost:5000/";

    public static JsonSerializerOptions CamelCaseSerializeOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}