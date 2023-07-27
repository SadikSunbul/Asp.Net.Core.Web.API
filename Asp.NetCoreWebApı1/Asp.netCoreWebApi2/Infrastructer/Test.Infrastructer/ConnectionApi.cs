using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Abstract.Services._Log;
using Test.Infrastructer.Services._Log;

namespace Test.Infrastructer
{
    public static class ConnectionApi
    {

        public static void AddInfrastructer(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerService, LoggerManager>();
            
        }
        public static void ConfigureJWT(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            services.AddAuthentication(opt =>
            { //kullanılıcak semalar token kullanıcaz dedık burada
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, //uretıcıyı dogrula
                    ValidateAudience = true, //gecerlı bır alıcımı onu dogrula
                    ValidateLifetime = true,//gecerlılıgını usre dogrula
                    ValidateIssuerSigningKey = true,//anahtarı dogrula
                    ValidIssuer = jwtSettings["validIssure"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
    }
}
