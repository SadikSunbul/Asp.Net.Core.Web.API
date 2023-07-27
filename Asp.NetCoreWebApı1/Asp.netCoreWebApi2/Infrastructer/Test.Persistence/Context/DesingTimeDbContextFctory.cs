using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test.Persistence.Context
{
    public class DesingTimeDbContextFctory : IDesignTimeDbContextFactory<TestContext>
    {
        public TestContext CreateDbContext(string[] args)
        {
            // configurationsBuilder

            var configuration = new ConfigurationBuilder() //using Microsoft.Extensions.Configuration;
                .SetBasePath(Directory.GetCurrentDirectory()) // buradakı path ın gelmesı ıcın Microsoft.AspNetCore.Mvc.NewtonsoftJson inmelidir
                .AddJsonFile("appsettings.Development.json").Build(); //burada erısım verdık oraya IConfuguratıon doner

            // DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<TestContext>().UseSqlServer(configuration.GetConnectionString("mssql"));
            
            return new TestContext(builder.Options);
        }
    }
}
