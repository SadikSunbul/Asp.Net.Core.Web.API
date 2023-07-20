using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public abstract class NotFountExeption : Exception
    {
        protected NotFountExeption(string message) : base(message)
        {

        }

    }
}
