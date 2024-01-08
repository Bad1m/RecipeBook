using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Security;

namespace RecipeMicroservice.API.Extensions
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
            switch (ex)
            {
                case KeyNotFoundException:
                    return HttpStatusCode.NotFound;

                case UnauthorizedAccessException:
                    return HttpStatusCode.Unauthorized;

                case SecurityException:
                    return HttpStatusCode.Forbidden;

                case ArgumentException:
                    return HttpStatusCode.BadRequest;

                case NotImplementedException:
                    return HttpStatusCode.NotImplemented;

                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}