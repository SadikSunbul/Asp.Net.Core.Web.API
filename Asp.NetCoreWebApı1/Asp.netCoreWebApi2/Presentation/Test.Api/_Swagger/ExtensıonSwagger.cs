using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Infrastructer.Services._Swagger
{
    public static class ExtensıonSwagger
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Sadık Sünbül"
                    ,
                        Version = "v1",
                        Description = "Sadık sünbül web api test",
                        TermsOfService = new Uri("https://github.com/SadikSunbul"),
                        Contact = new OpenApiContact
                        {
                            Name = "Sadık Sünbül",
                            Email = "sadık.sunbul@mail.com",
                            Url = new Uri("https://github.com/SadikSunbul")

                        }
                    });
                s.SwaggerDoc("v2", new OpenApiInfo { Title = "Sadık Sünbül", Version = "v2" });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                //oturum acma ıslemlerı ıcın yazıldı 
                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference=new OpenApiReference()
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Name="Baerer"
                        },
                        new List<string>()
                    }
                });

            });
        }

    }
}
