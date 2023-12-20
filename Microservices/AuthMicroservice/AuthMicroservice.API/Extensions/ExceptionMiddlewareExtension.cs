using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

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
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (ex != null)
                    {
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = ex.Message }));
                    }
                });
            });

            return app;
        }
    }
}