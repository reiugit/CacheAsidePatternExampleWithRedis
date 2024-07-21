namespace CacheAsidePatternExampleWithIMemoryCache;

public record ResponseWithCacheInfo(int Id, string Response, bool WasCached, double CachedSinceInSeconds);
