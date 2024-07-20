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
    var (wasCached, response) = await cache.GetOrCreateAsync(id, () => $"Response for Id {id}");

    return new { id, response, wasCached };
});

app.Run();
