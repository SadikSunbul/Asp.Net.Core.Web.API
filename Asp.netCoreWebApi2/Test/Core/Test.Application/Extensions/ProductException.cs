using Microsoft.AspNetCore.HttpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Test.Domain.Entites;

namespace Test.Application.Extensions
{
    public static class ProductException
    {

        public static IQueryable<Product> Sort(this IQueryable<Product> product, string groubBystring)
        {
            if (groubBystring is null) return product.OrderBy(i => i.Id);
            var groubByarr = groubBystring.Trim().Split(',');
            var propertInfo = typeof(Product)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderqueryBuilder = new StringBuilder();

            foreach (var param in groubByarr)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertInfo.FirstOrDefault(p => p.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty is null) continue;
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderqueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");

            }
            var orderQuery = orderqueryBuilder.ToString().TrimEnd(',', ' ');
            if (orderQuery is null) return product.OrderBy(i => i.Id);
            return product.OrderBy(orderQuery);
        }
    }
}
