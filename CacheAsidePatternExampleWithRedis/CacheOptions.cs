using Microsoft.Extensions.Caching.Distributed;

namespace CacheAsidePatternExampleWithIDistributedCache;

public static class CacheOptions //for brevity without options pattern and without DI
{
    private static readonly DistributedCacheEntryOptions absoluteExpirationInFiveSeconds = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
    };

    public static DistributedCacheEntryOptions AbsoluteExpirationInFiveSeconds
        => absoluteExpirationInFiveSeconds;
}
