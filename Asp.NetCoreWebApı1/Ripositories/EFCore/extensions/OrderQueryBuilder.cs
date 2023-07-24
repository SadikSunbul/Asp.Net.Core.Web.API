using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ripositories.EFCore.extensions
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            var orderparams = orderByQueryString.Trim().Split(","); //boslukları at virgül ile ayır
            //?orderBy=title,price ---> ilk titleye gore sonra fiyata göre sırala dediondan [0]=title [1]=price
            var propertyInfod = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance); //Book clasının propertyleri alınır publig veya newlene bılenlerı getirir

            var orderqueryBuilder = new StringBuilder();

            foreach (var param in orderparams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }
                var propertyFromQueryName = param.Split(" ")[0];
                var objectPropery = propertyInfod
                    .FirstOrDefault(p => p.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectPropery is null)
                {
                    continue;
                }
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderqueryBuilder.Append($"{objectPropery.Name.ToString()} {direction}");
            }
            var orderQuery = orderByQueryString.ToString().TrimEnd(',', ' ');
            return orderQuery;
        }
    }
}
