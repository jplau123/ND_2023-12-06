using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ND_2023_12_06.DTOs;
using ND_2023_12_06.Exceptions;
using System.Text.Json;
using System.Threading.Tasks;

namespace ND_2023_12_06.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public AuthMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string? myKey = httpContext.Request.Headers["ApiKey"];
            string? mySecret = _config.GetValue<string>("MyApiKey");

            if (myKey == null)
            {
                throw new ApiKeyNotFoundException("Api-key is empty.");
            }

            if (mySecret == null)
            {
                throw new ApiKeyNotFoundException("Cannot access Api Key in AppSettings.json");
            }

            if (myKey != mySecret)
            {
                throw new UnauthorizedAccessException("Your provided API key is wrong.");
            }

            return _next(httpContext);
        }
    }
}
