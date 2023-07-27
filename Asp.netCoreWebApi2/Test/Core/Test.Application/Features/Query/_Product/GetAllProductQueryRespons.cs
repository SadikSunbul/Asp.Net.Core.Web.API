using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entites;

namespace Test.Application.Features.Query._Product
{
    public class GetAllProductQueryRespons
    {
        public IEnumerable<Product> produks { get; set; }
        public Ayrıntılar ayrıntı { get; set; }
    }
    public class Ayrıntılar
    {
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool HasPrevious => PageSize > 1;
        public bool HasPage => PageSize < TotalPage;
    }
}
