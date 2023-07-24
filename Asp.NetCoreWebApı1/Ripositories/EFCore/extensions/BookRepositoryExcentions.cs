using Entities.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Ripositories.EFCore.extensions
{
    public static class BookRepositoryExcentions
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice) => books.Where(books => books.Price >= minPrice && books.Price <= maxPrice);


        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return books;
            var lowweCaseTerm = searchTerm.Trim().ToLower(); //basında sonundakı bosluklar atıldı ve kucuk harfe donuldu
            return books
                .Where(b => b.Title.ToLower().Contains(lowweCaseTerm));
        }


        public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return books.OrderBy(b => b.Id);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Book>(orderByQueryString);

            if (orderQuery is null)
            {
                return books.OrderBy(b => b.Id);
            }
            return books.OrderBy(orderQuery); //using System.Linq.Dynamic.Core;
        }
    }
}
