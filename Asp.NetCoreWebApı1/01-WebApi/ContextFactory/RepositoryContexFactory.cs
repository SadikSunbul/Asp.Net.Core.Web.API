using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Ripositories.EFCore;

namespace _01_WebApi.ContextFactory
{
    public class RepositoryContexFactory : IDesignTimeDbContextFactory<RepositoriesContext>
    {
        public RepositoriesContext CreateDbContext(string[] args)
        {
            // configurationsBuilder

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json").Build(); //burada erısım verdık oraya IConfuguratıon doner

            // DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<RepositoriesContext>().UseSqlServer(configuration.GetConnectionString("mssql"),
                prj=>prj.MigrationsAssembly("01-WebApi")) ;
            // prj=>prj.MigrationsAssembly("01-WebApi") migrateler 01-WebApi burada olusturulsun dedık

            return new RepositoriesContext(builder.Options);
        }
    }
}
