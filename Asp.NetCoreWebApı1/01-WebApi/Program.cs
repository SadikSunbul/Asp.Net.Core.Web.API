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
    .AddApplicationPart(typeof(Presentation.AsemblyRefence).Assembly) //buaradak� kodda ap� k�s�mlar�n� farkl� yerde yaz�cag�m �c�n oran�n assambly k�sm�n� verd�k buraya ordan bulup al�cak 
    .AddNewtonsoftJson();


//LoadConfiguration config yukle 
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config")); //String.Concat birle�tir 

// Directory.GetCurrentDirectory() bu klasor nerde cals�yor �se onu al ded�k
builder.Services.ConfigureLoggerService();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigreSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigurationServiceManager();

var app = builder.Build();

var logger=app.Services.GetRequiredService<ILogerService>(); //bu servise ihtiyac�m var dedik bunu bana kontrattan ��z getir dedik 


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

app.UseAuthorization();

app.MapControllers();

app.Run();
