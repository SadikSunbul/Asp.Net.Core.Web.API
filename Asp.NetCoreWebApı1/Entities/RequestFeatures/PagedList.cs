using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }
        public PagedList(List<T> items, int count, int pagenumber, int pagesize)
        {
            MetaData = new MetaData()
            {
                PageSize = pagesize,
                TotalCount = count,
                CurrentPage = pagesize,
                TotalPage = (int)Math.Ceiling(count / (double)pagesize)
            };
            AddRange(items);
        }
        public static PagedList<T> ToPagedLİst(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
