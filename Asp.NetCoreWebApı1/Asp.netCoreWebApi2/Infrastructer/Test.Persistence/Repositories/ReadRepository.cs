using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Repository;
using Test.Domain.Entites.Common;
using Test.Persistence.Context;

namespace Test.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntiey
    {
        private readonly TestContext context;

        public ReadRepository(TestContext context)
        {
            this.context = context;
        }

        public DbSet<T> Table => context.Set<T>();

        public IQueryable<T> GetAll(bool changetracer = true)
        {
            var data = Table.AsQueryable();
            if (!changetracer)
            {
                 data.AsNoTracking();
            }
            return data;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> func, bool changetracer = true)
        {
            var query = Table.AsQueryable();
            if (!changetracer)
            {
                query = query.AsNoTracking(); //trackıng devredısı oldu 
            }
            return await query.FirstOrDefaultAsync(func);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> func, bool changetracer = true)
        {
            var query = Table.Where(func);
            if (!changetracer)
            {
                query = query.AsNoTracking(); //trackıng devredısı oldu 
            }
            return query;
        }

        public async Task<T> GetById(string id, bool changetracer = true)
        {
            var query = Table.AsQueryable();
            if (!changetracer)
            {
                query = query.AsNoTracking(); //trackıng devredısı oldu 
            }
            return await query.FirstOrDefaultAsync(i => i.Id == Guid.Parse(id));
        }
    }
}
