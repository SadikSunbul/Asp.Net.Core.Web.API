using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entites.Common;

namespace Test.Application.Repository
{
    public interface IRepository<T> where T : BaseEntiey
    {
        DbSet<T> Table { get; }
    }
}
