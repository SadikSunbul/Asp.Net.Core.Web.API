using Microsoft.EntityFrameworkCore;
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
    }
}
