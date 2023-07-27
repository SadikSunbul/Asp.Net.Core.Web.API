using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Abstract.Services._Log;
using Test.Domain.Entites;

namespace Test.Application.Features.Commends._User.CreateUser
{
    public class CreateUserCommendHandler : IRequestHandler<CreateUserCommendRequest, CreateUserCommendRespons>
    {
        private readonly ILoggerService loger;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public CreateUserCommendHandler(ILoggerService loger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            this.loger = loger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<CreateUserCommendRespons> Handle(CreateUserCommendRequest request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request);
            var resultt = await userManager
                .CreateAsync(user, request.Password);

            if (resultt.Succeeded) //basarılı ıse kayıt gır
            {
                await userManager.AddToRolesAsync(user, request.Roles); //ral tanımları 
            }
            return new CreateUserCommendRespons() { result=resultt };
        }
    }
}
