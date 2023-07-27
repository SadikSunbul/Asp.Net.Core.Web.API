using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Abstract.Services._Log;
using Test.Application.ActiponFilter;
using Test.Application.Repository;
using Test.Domain.Entites;

namespace Test.Application.Features.Commends._Product.CreateProduct
{
    public class CreateProductCommendHandler : IRequestHandler<CreateProductCommendRequest, CreateProductCommendRespons>
    {
        private readonly IProductWriteRepository productWrite;
        private readonly ILoggerService logger;
        private readonly IMapper mapper;
        

        public CreateProductCommendHandler(IProductWriteRepository productWrite, ILoggerService logger, IMapper mapper)
        {
            this.productWrite = productWrite;
            this.logger = logger;
            this.mapper = mapper;
            
        }
        public async Task<CreateProductCommendRespons> Handle(CreateProductCommendRequest request, CancellationToken cancellationToken)
        {
            
            var data = await productWrite.AddAsync(mapper.Map<Product>(request)); //mapper.Map<Product>(request) auto maper ayarlar

            await productWrite.SaveAsync();
            logger.LogInfo($"{request.Name}-> isimli ürün kayıt edildi");
            return new CreateProductCommendRespons()
            {
                Success = data
            };

        }
    }
}
