using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.BusinessLogic.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace AuthMicroservice.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                            await context.Response.WriteAsync(
                                new ResponseModel()
                                {
                                    StatusCode = context.Response.StatusCode,
                                    Message = "Internal Server Error."
                                }.ToString());
                        }
                    });
                });
        }
    }
}