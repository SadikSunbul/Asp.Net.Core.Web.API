using Entities.DTO_DataTransferObject_;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Presentation.ActionFilter;
using Presentation.Controllers;
using Ripositories.Contracts;
using Ripositories.EFCore;
using Services;
using Services.Contrant;

namespace _01_WebApi.extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigreSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoriesContext>(o => o.UseSqlServer(configuration.GetConnectionString("mssql")));

        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigurationServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILogerService, LogerManager>();
        }
        public static void ConfigurActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribut>();
            services.AddSingleton<LoFilterAttribute>();
            services.AddScoped<ValidateMediaTypeAtribut>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()//tum kokenlere ızın ver
                            .AllowAnyMethod() //tum metotlara ızın ver
                            .AllowAnyHeader()//tum headerlara ızın ver 
                            .WithExposedHeaders("X-Pagination");
                });
            });
        }
        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<BookDto>, DataShaper<BookDto>>();
        }

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config
                .OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();
                if (systemTextJsonOutputFormatter is not null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.btkakademi.hateoas+json");

                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.btkakademi.apiroot+json");
                }

                var xmlOutputFormatter = config
                .OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                if (xmlOutputFormatter is not null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.btkakademi.hateoas+xml");

                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.btkakademi.apiroot+xml");
                }
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true; //apı versıon bılgısını respons header kısmına eklıyoruz
                opt.AssumeDefaultVersionWhenUnspecified = true; //Kullanıcı herhangıbır versıyon bılgısı talep etmezse default versıyon u verır 
                opt.DefaultApiVersion = new ApiVersion(1, 0);//default versıonun hne oldugunu soylerız(1.0) buyuk degısıklıkler 1 kucuk degısıklıkler 0 dedik
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
                opt.Conventions.Controller<BooksKontroller>()
                .HasApiVersion(new ApiVersion(1, 0));

                opt.Conventions.Controller<BooksV2Controller>()
                .HasDeprecatedApiVersion(new ApiVersion(2, 0));

            });
        }

        public static void ConfugerResponseCaching(this IServiceCollection services) => services.AddResponseCaching();
    }
}
