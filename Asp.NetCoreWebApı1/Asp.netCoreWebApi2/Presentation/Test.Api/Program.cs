using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLog;
using Test.Application;
using Test.Application.Abstract.Services._Log;
using Test.Application.Extensions;
using Test.Application.Validater._Product;
using Test.Infrastructer;
using Test.Infrastructer.Services._Swagger;
using Test.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
})
    //.AddXmlDataContractSerializerFormatters()
    .AddNewtonsoftJson()
    .AddFluentValidation(c=>c.RegisterValidatorsFromAssemblyContaining<CreateProductCommendRequestValidation>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen(); //bunu extens�onsa yad�k
builder.Services.ConfigureSwagger();

builder.Services.AddPersistence(builder.Configuration.GetConnectionString("mssql"));

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddInfrastructer();

builder.Services.AddApplication();

builder.Services.BuildServiceProvider();

builder.Services.ConfigureJWT(builder.Configuration);


var app = builder.Build();


var log = app.Services.GetRequiredService<ILoggerService>();

app.ConfigureExeptionHandler(log);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Sad�k S�nb�l 1");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "Sad�k S�nb�l 2");
    });
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();//kullan�c� ad� s�fre �le dogrulama gercekelscek sonra yetk�lend�rme yap�ls�n
app.UseAuthorization();//Yetkilendirme

app.MapControllers();

app.Run();
