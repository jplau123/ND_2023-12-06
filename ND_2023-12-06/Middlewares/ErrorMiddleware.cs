using ND_2023_12_06.DTOs;
using ND_2023_12_06.Exceptions;
using System.Text.Json;

namespace ND_2023_12_06.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
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
            catch (Exception ex)
            {
                statusCode = 400;
                message = $"{ex.Message}";
            }

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

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorMiddleware>();
        }
    }
}
