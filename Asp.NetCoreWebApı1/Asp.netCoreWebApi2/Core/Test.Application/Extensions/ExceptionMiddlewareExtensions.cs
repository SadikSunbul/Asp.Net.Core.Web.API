using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Abstract.Services._Log;
using Test.Domain.Entites.ErrorModel;

namespace Test.Application.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {

        public static void ConfigureExeptionHandler(this WebApplication app, ILoggerService logger)
        {

            app.UseExceptionHandler(e =>
            {
                e.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contexFeatur = context.Features.Get<IExceptionHandlerFeature>();
                    if (context is not null)
                    {
                        logger.LogInfo($"Servise baglanırken kata oldu kod:{context.Response.StatusCode}-eror:{contexFeatur.Error}");
                        await context.Response.WriteAsync(
                            new ErorrDetails()
                            {
                                Message = "Servise Bağlanma hatası var",
                                StatusCode = context.Response.StatusCode
                            }.ToString()
                            );
                    }
                });
            });

        }
    }
}
