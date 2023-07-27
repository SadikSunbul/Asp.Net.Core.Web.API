using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.ActiponFilter;
using Test.Application.Extensions;
using Test.Application.Repository;

namespace Test.Application.Features.Query._Product
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryRespons>
    {
        private readonly IProductReadRepository productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            this.productReadRepository = productReadRepository;
        }

       
        public async Task<GetAllProductQueryRespons> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {

            var data = await productReadRepository.GetAll()
                 .Sort(request.GroupBy)
                 .Skip((request.PageNumber - 1) * request.PageSize)
                 .Take(request.PageSize)
                 .ToListAsync();
            var count = productReadRepository.GetAll().Count();

            return new GetAllProductQueryRespons()
            {
                produks = data,
                ayrıntı = new Ayrıntılar()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalPage = (int)Math.Ceiling(count / (double)request.PageSize)
                }
            };
        }
    }
}
