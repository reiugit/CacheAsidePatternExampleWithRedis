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
    return await cache.GetOrCreateWithCacheInfoAsync(id, factory: () => $"Response for Id {id} ...");
});

app.Run();
