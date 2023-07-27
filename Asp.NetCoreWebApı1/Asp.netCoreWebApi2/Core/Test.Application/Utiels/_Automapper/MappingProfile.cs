using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Features.Commends._Product.CreateProduct;
using Test.Application.Features.Commends._User.CreateUser;
using Test.Domain.Entites;

namespace Test.Application.Utiels._Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductCommendRequest, Product>();
            CreateMap<CreateUserCommendRequest, User>();

        }
    }
}
