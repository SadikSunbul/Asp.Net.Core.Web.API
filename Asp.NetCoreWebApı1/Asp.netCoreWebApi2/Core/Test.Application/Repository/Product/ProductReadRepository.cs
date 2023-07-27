using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entites;

namespace Test.Application.Repository
{
    public interface IProductReadRepository :IReadRepository<Product>
    {
    }
}
