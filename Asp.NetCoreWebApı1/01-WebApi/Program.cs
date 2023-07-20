using _01_WebApi.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using Ripositories.EFCore;
using Services.Contrant;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AsemblyRefence).Assembly) //buaradaký kodda apý kýsýmlarýný farklý yerde yazýcagým ýcýn oranýn assambly kýsmýný verdýk buraya ordan bulup alýcak 
    .AddNewtonsoftJson();


//LoadConfiguration config yukle 
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config")); //String.Concat birleþtir 

// Directory.GetCurrentDirectory() bu klasor nerde calsýyor ýse onu al dedýk
builder.Services.ConfigureLoggerService();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigreSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigurationServiceManager();

var app = builder.Build();

var logger=app.Services.GetRequiredService<ILogerService>(); //bu servise ihtiyacým var dedik bunu bana kontrattan çöz getir dedik 


app.ConfigureExeptiomHandler(logger);//usteký logerý verdik


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction()) //productýon ortamýnda ýse 
{
    app.UseHsts();//detaylar konusulcak
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
