using _01_WebApi.extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog;
using Presentation.ActionFilter;
using Ripositories.EFCore;
using Services;
using Services.Contrant;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;//default u false içerik pazarlýðýný açar
    config.ReturnHttpNotAcceptable = true; //kabul etmedýgýmýz formatlarý al dedik
})
    .AddXmlDataContractSerializerFormatters() //bunu ekleyince artýk xml formatýnda cýkýs verebýlýrýz
    .AddCustomCsvFormatter() //csv formatýnda cýktý verir ama gerek yok 
    .AddApplicationPart(typeof(Presentation.AsemblyRefence).Assembly); //buaradaký kodda apý kýsýmlarýný farklý yerde yazýcagým ýcýn oranýn assambly kýsmýný verdýk buraya ordan bulup alýcak 
    //.AddNewtonsoftJson();





//LoadConfiguration config yukle 
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config")); //String.Concat birleþtir 

// Directory.GetCurrentDirectory() bu klasor nerde calsýyor ýse onu al dedýk
builder.Services.ConfigureLoggerService();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    //default olaný devre dýsý brakýr
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigreSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigurationServiceManager();
builder.Services.ConfigurActionFilters();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureCors();
builder.Services.ConfigureDataShaper();
builder.Services.AddCustomMediaTypes();

builder.Services.AddScoped<LinkGenerator>();
builder.Services.AddScoped<IBookLinks, BookLinks>();



var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogerService>(); //bu servise ihtiyacým var dedik bunu bana kontrattan çöz getir dedik 


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

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
