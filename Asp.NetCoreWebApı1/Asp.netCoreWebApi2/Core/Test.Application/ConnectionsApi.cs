using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Abstract;
using Test.Application.ActiponFilter;
using Test.Application.Features.Commends._Product.CreateProduct;
using Test.Application.Validater._Product;

namespace Test.Application
{
    public static class ConnectionsApi
    {

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateProductCommendHandler));
            services.AddHttpClient(); //using Microsoft.Extensions.DependencyInjection;
            services.AddAutoMapper(typeof(CreateProductCommendRequest));


            services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateProductCommendRequestValidation>());



            services.AddScoped<IsValidationFilter>();

            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.SuppressModelStateInvalidFilter = true;
            });

        }
    }
}
