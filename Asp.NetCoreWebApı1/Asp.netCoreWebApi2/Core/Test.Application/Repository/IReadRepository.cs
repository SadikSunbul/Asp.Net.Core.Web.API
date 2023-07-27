using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entites.Common;

namespace Test.Application.Repository
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntiey
    {
        IQueryable<T> GetAll(bool changetracer = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> func, bool changetracer = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> func, bool changetracer = true);
        Task<T> GetById(string id, bool changetracer = true);
    }
}
