using CacheAsidePatternExampleWithRedis;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace CacheAsidePatternExampleWithIDistributedCache;

public static class CacheExtensions
{
    public static async Task<(string response, bool wasCached, double cachedSinceInSeconds)> GetOrCreateWithCacheInfoAsync(this IDistributedCache cache, int id, Func<string> stringFactory)
    {
        string? cachedJson;

        try
        {
            cachedJson = await cache.GetStringAsync(id.ToString());
        }
        catch (RedisConnectionException)
        {
            return ("Redis must run locally!  Please start a Redis docker container:  docker run -p 6379:6379 --name redis -d redis", false, 0);
        }

        if (!string.IsNullOrWhiteSpace(cachedJson))
        {
            var cachedResponseWithTimestamp = JsonSerializer.Deserialize<ResponseWithTimestamp>(cachedJson);

            var cachedSinceInSeconds = DateTimeOffset.UtcNow
                .Subtract(cachedResponseWithTimestamp!.CachedAt)
                .Seconds;

            return (cachedResponseWithTimestamp!.Response, true, cachedSinceInSeconds);
        }

        var response = stringFactory();

        var responseWithTimestamp = new ResponseWithTimestamp(response, DateTimeOffset.UtcNow);
        var json = JsonSerializer.Serialize(responseWithTimestamp);

        cache.SetString(id.ToString(), json, CacheOptions.AbsoluteExpirationInFiveSeconds);

        return (response, false, 0);
    }
}
