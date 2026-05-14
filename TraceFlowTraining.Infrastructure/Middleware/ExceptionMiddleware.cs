using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace TraceFlowTraining.Infrastructure.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception)
        {
            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                message = "An unexpected error occurred",
                statusCode = context.Response.StatusCode,
                timestamp = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}