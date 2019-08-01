using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using NServiceBus.Logging;

namespace Bondora.ConstructionEquipmentRental.Service.Middleware
{
    public class RequestLoggingMiddleware
    {
        private static readonly ILog Log = LogManager.GetLogger<RequestLoggingMiddleware>();
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            HttpRequest httpContextRequest = context.Request;
            string requestName = $"{httpContextRequest.Method} {httpContextRequest.Path}";
            string displayUrl = httpContextRequest.GetDisplayUrl();

            Log.Info($"{requestName} with url {displayUrl} started.");

            await _next(context);

            int statusCode = context.Response.StatusCode;

            Log.Info($"{requestName} stopped with status code - {statusCode}.");
        }
    }
}
