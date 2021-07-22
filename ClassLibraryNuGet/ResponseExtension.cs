using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibraryNuGet
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    public static class ResponseExtension
    {
        public static void AddMessage(this Startup.Response response, string message)
        {
            response.Message = $"{DateTime.Now}: {message}";
        }

        public static void UseHealthChecker(this IEndpointRouteBuilder endpoints, Func<string> response)
        {
            endpoints.MapGet("/health", async context =>
            {
                await context.Response.WriteAsync(response());
            });

        }
    }
}
