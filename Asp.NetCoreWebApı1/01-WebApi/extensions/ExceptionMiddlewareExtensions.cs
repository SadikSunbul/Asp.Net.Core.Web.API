﻿using Entities.ErorModel;
using Microsoft.AspNetCore.Diagnostics;
using Services.Contrant;
using System.Net;

namespace _01_WebApi.extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExeptiomHandler(this WebApplication app, ILogerService loger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500 kodu hangı hata turu olursa olsun ılk basta 500 kabul edıcez 
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>(); //böyle bir özellik varmı hata olusturan bısey varmı dıye kontrol eder burası 
                    if (contextFeature is not null) //var ise hata var demektir
                    {
                        loger.LogEror($"Something went wrong:{contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Servera baglanamadı"
                        }.ToString()); //buranın to strıngını overıde atmıstık 
                    }
                });
            });
        }

    }
}
