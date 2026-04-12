using System.Text.Json;

namespace Lingua.BuildingBlocks.Caching.Internal.Helpers;

/// <summary>
/// Simple helper class to estimate the size of an object in bytes by serializing it to JSON and measuring the length of the resulting string (Not 100% accurate).
/// </summary>
public static class SizeEstimator
{
    /// <summary>
    /// Estimate the size of an object in bytes by serializing it to JSON and measuring the length of the resulting string (Not 100% accurate).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static long Estimate<T>(T value)
    {
        if (value is null) return 0;
        return JsonSerializer.Serialize(value).Length;
    }
}

