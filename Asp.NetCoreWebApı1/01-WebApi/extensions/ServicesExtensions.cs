using Entities.DTO_DataTransferObject_;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Presentation.ActionFilter;
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
    }
}
