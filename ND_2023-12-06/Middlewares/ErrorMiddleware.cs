using ND_2023_12_06.DTOs;
using ND_2023_12_06.Exceptions;
using System.Text.Json;

namespace ND_2023_12_06.Middlewares;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorMiddleware> _logger;

    public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        int statusCode;
        string message;

        try
        {
            await _next(httpContext);
            return;
        }
        catch (ApiKeyNotFoundException ex)
        {
            statusCode = 404;
            message = $"API key could not be found: {ex.Message}";
        }
        catch (UnauthorizedAccessException ex)
        {
            statusCode = 401;
            message = $"{ex.Message}";
        }
        catch (KeyNotFoundException ex)
        {
            statusCode = 404;
            message = $"{ex.Message}";
        }
        catch (Exception)
        {
            statusCode = 400;
            message = $"Unexpected exception was thrown. Please try again.";
        }

        _logger.Log(LogLevel.Error, $"Error: {message}");

        httpContext.Response.StatusCode = statusCode;

        var response = new ErrorViewModel
        {
            status = statusCode,
            message = message
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        return;
    }
}