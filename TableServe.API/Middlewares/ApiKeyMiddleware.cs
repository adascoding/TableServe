using TableServe.API.Services;

namespace TableServe.API.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-Api-Key";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IWaiterService waiterService)
    {
        var path = context.Request.Path.Value;
        if (path.StartsWith("/api/Waiters"))
        {
            await _next(context);
            return;
        }
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is missing.");
            return;
        }

        var waiter = await waiterService.GetWaiterByApiKeyAsync(extractedApiKey);
        if (waiter == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API Key.");
            return;
        }

        await _next(context);
    }
}