using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entites.Common;

namespace Test.Domain.Entites
{
    public class Product : BaseEntiey
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

    }
}
