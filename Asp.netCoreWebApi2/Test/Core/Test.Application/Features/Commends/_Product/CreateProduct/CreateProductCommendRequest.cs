using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Application.Features.Commends._Product.CreateProduct
{
    public class CreateProductCommendRequest:IRequest<CreateProductCommendRespons>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
