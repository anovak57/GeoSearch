using GeoSearch.BusinessLogicLayer.ServiceContracts;

namespace GeoSearch.API.Middlewares;

public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        
        if (!context.Request.Headers.TryGetValue("x-api-key", out var apiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is missing.");
            return;
        }

        var user = await userService.GetUserByApiKey(apiKey);

        if (user is null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API Key.");
            return;
        }
        
        context.Items["User"] = user;
        
        await _next(context);
    }
}