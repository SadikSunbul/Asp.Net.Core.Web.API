using _01_WebApi.extensions;
using AspNetCoreRateLimit;
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
    config.RespectBrowserAcceptHeader = true;//default u false i�erik pazarl���n� a�ar
    config.ReturnHttpNotAcceptable = true; //kabul etmed�g�m�z formatlar� al dedik
    config.CacheProfiles.Add("5mins",new CacheProfile() { Duration=300}); //yeni profil isim ve nesne uret�lr�
})
    .AddXmlDataContractSerializerFormatters() //bunu ekleyince art�k xml format�nda c�k�s vereb�l�r�z
    .AddCustomCsvFormatter() //csv format�nda c�kt� verir ama gerek yok 
    .AddApplicationPart(typeof(Presentation.AsemblyRefence).Assembly); //buaradak� kodda ap� k�s�mlar�n� farkl� yerde yaz�cag�m �c�n oran�n assambly k�sm�n� verd�k buraya ordan bulup al�cak 
    //.AddNewtonsoftJson();





//LoadConfiguration config yukle 
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config")); //String.Concat birle�tir 

// Directory.GetCurrentDirectory() bu klasor nerde cals�yor �se onu al ded�k
builder.Services.ConfigureLoggerService();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    //default olan� devre d�s� brak�r
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

builder.Services.ConfigureVersioning();
builder.Services.ConfugerResponseCaching();
builder.Services.ConfigureHttpCachHeaders();

builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();  

builder.Services.AddMemoryCache();//Istekler� saycag�m�z �c�n buna �ht�yac�m�z var 

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogerService>(); //bu servise ihtiyac�m var dedik bunu bana kontrattan ��z getir dedik 


app.ConfigureExeptiomHandler(logger);//ustek� loger� verdik


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction()) //product�on ortam�nda �se 
{
    app.UseHsts();//detaylar konusulcak
}

app.UseHttpsRedirection();

app.UseIpRateLimiting();

app.UseCors("CorsPolicy");
app.UseResponseCaching(); //Corstan sonra cach�ng cagr�lmas� oner�l�r
app.UseHttpCacheHeaders();


app.UseAuthorization();

app.MapControllers();

app.Run();
