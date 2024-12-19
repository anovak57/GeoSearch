using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace GeoSearch.API.Filters;

public class IdempotencyFilter : IAsyncActionFilter
{
    // Only using In-Memory cache for development purposes, would swap out with Redis for production
    private readonly IMemoryCache _cache;

    public IdempotencyFilter(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idempotencyKey = context.HttpContext.Request.Headers["Idempotency-Key"].FirstOrDefault();

        if (string.IsNullOrEmpty(idempotencyKey))
        {
            await next();
            return;
        }

        if (_cache.TryGetValue(idempotencyKey, out string cachedResult))
        {
            context.Result = new ContentResult
            {
                Content = cachedResult,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK
            };
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is ObjectResult objectResult)
        {
            var serializedResult = JsonSerializer.Serialize(objectResult.Value);

            // Check for new locations every 60 minutes for the same query, as this is subject to change
            _cache.Set(idempotencyKey, serializedResult, TimeSpan.FromMinutes(60));

            context.Result = new ContentResult
            {
                Content = serializedResult,
                ContentType = "application/json",
                StatusCode = objectResult.StatusCode ?? StatusCodes.Status200OK
            };
        }
    }
}