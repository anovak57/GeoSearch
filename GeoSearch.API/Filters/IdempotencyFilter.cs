using GeoSearch.API.SignalR.ServiceContracts;
using GeoSearch.BusinessLogicLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace GeoSearch.API.Filters;

public class IdempotencyFilter : IAsyncActionFilter
{
    // Only using In-Memory cache for development purposes, would swap out with Redis for production
    private readonly IMemoryCache _cache;
    private readonly ISignalRNotifier _signalRNotifier;

    public IdempotencyFilter(IMemoryCache cache, ISignalRNotifier signalRNotifier)
    {
        _cache = cache;
        _signalRNotifier = signalRNotifier;
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
            var searchResult = JsonConvert.DeserializeObject<SearchResult>(cachedResult);

            if (searchResult != null)
            {
                await _signalRNotifier.NotifySearchRequestAsync(searchResult);
            }

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
            var serializedResult = JsonConvert.SerializeObject(objectResult.Value);

            // Check for new locations every 60 minutes for the same query, as this is subject to change
            _cache.Set(idempotencyKey, serializedResult, TimeSpan.FromMinutes(60));

            if (objectResult.Value is SearchResult searchResult)
            {
                await _signalRNotifier.NotifySearchRequestAsync(searchResult);
            }

            context.Result = new ContentResult
            {
                Content = serializedResult,
                ContentType = "application/json",
                StatusCode = objectResult.StatusCode ?? StatusCodes.Status200OK
            };
        }
    }
}
