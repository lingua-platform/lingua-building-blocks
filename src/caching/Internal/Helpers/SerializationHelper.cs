using System.Text.Json;

namespace Lingua.BuildingBlocks.Caching.Internal.Helpers;

/// <summary>
/// Serialization helper class for caching
/// </summary>
public static class SerializationHelper
{
    /// <summary>
    /// Serializes an object to a JSON string
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string Serialize<T>(T obj)
        => JsonSerializer.Serialize(obj);

    /// <summary>
    /// Deserializes a JSON string to an object of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static T Deserialize<T>(string json)
        => JsonSerializer.Deserialize<T>(json)!;
}
