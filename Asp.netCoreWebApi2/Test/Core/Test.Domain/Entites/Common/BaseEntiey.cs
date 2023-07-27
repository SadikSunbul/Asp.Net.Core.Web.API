using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain.Entites.Common
{
    public class BaseEntiey
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        virtual public DateTime UpdateDate { get; set; }

    }
}
