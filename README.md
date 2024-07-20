# CacheAsidePatternExampleWithRedis

This example uses 'StackExchangeRedis' as an implementation of IDistributedCache.

Topics:

    Adding StackExchangeRedisCache to DI Container
    Injecting IDistributedCache into endpoint
    Usage of 'IDistributedCache.GetString(key)'
    Usage of 'IDistributedCache.SetString(key, string, CacheOptions)'
    Expiration/Eviction

This can be used to cache serialized objects in the form of json strings.
