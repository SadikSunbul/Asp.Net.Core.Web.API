using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Repository;
using Test.Domain.Entites;
using Test.Persistence.Context;

namespace Test.Persistence.Repositories._Product
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(TestContext context) : base(context)
        {
        }
    }
}
