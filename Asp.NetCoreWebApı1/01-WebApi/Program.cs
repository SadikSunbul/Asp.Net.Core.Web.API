using _01_WebApi.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ripositories.EFCore;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AsemblyRefence).Assembly) //buaradak� kodda ap� k�s�mlar�n� farkl� yerde yaz�cag�m �c�n oran�n assambly k�sm�n� verd�k buraya ordan bulup al�cak 
    .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigreSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigurationServiceManager();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
