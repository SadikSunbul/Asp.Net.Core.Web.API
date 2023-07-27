using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Application.Features.Query._Product
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryRespons>
    {
        const int MaxResults = 15;
        public int PageNumber { get; set; } = 1;

        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxResults ? MaxResults : value; }
        }

        public string? GroupBy { get; set; }

    }
}
