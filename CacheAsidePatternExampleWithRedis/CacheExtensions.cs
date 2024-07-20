using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace CacheAsidePatternExampleWithIDistributedCache;

public static class CacheExtensions
{
    public static async Task<(bool wasCached, string item)> GetOrCreateAsync(this IDistributedCache cache, int id, Func<string> stringFactory)
    {
        bool wasCached = true;

        string? response;

        try
        {
            response = await cache.GetStringAsync(id.ToString());
        }
        catch (RedisConnectionException)
        {
            return (false, "Redis must run locally!  Please start a docker container with Redis:  docker run -p 6379:6379 --name redis -d redis");
        }

        if (string.IsNullOrWhiteSpace(response))
        {
            wasCached = false;

            response = stringFactory();

            cache.SetString(id.ToString(), response, CacheOptions.AbsoluteExpirationInFiveSeconds);
        }

        return (wasCached, response);
    }
}
