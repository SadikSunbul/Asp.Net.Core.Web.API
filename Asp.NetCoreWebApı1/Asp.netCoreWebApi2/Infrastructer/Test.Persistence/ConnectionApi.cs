using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Repository;
using Test.Domain.Entites;
using Test.Persistence.Context;
using Test.Persistence.Repositories._Product;

namespace Test.Persistence
{
    public static class ConnectionApi
    {

        public static void AddPersistence(this IServiceCollection services, string connectionstring)
        {
            services.AddDbContext<TestContext>(i => i.UseSqlServer(connectionstring));
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();

            //Kullanıcı adı sıfre mıdle verı aktıflestırme
            services.AddAuthentication();

            //Identıty ı confugrre edıcez
            var builder = services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = true; //rakam ıstıyormuyuz
                opts.Password.RequireLowercase = false;//kucuk harf ıstıyormu
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;//ozelkarakter ıstıyormuyuz
                opts.Password.RequiredLength = 6; //uznluk 6 olsun
                
                opts.User.RequireUniqueEmail = true;//1 emaıl 1 kere kullanılsın
            })
                .AddEntityFrameworkStores<TestContext>()
                .AddDefaultTokenProviders();
        }
    }
}
