using ND_2023_12_06.Middlewares;

namespace ND_2023_12_06.Extensions;

public static class ErrorMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorMiddleware>();
    }
}
