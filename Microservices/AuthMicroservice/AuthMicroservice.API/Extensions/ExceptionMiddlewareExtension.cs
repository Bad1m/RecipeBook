using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Security;

namespace AuthMicroservice.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (ex != null)
                    {
                        var statusCode = GetStatusCode(ex);
                        context.Response.StatusCode = (int)statusCode;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = ex.Message }));
                    }
                });
            });

            return app;
        }

        private static HttpStatusCode GetStatusCode(Exception ex)
        {
            return ex switch
            {
                KeyNotFoundException => HttpStatusCode.NotFound,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                SecurityException => HttpStatusCode.Forbidden,
                ArgumentException => HttpStatusCode.BadRequest,
                NotImplementedException => HttpStatusCode.NotImplemented,
                _ => HttpStatusCode.InternalServerError,
            };
        }
    }
}