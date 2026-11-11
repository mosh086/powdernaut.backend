using System.Text.Json;

namespace Powdernaut.Application.Utilities.Extensions;

public static class JsonExtentions
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public static string ToJson(this object obj)
    {
        return JsonSerializer.Serialize(obj, SerializerOptions);
    }

    public static T To<T>(this string str)
    {
        return JsonSerializer.Deserialize<T>(str, SerializerOptions)!;
    }
}