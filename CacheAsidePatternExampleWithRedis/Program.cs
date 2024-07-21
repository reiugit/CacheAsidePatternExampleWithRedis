using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using CacheAsidePatternExampleWithIDistributedCache;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(setupAction =>
{
    setupAction.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var app = builder.Build();

app.MapGet("/test-cacheaside/{id:int}", async (int id, [FromServices] IDistributedCache cache) =>
{
    var (response, wasCachedWithRedis, cachedSinceInSeconds) = await cache.GetOrCreateWithCacheInfoAsync(id, () => $"Response for Id {id}");

    return new { id, response, wasCachedWithRedis, cachedSinceInSeconds };
});

app.Run();
