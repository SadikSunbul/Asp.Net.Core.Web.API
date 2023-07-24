using Microsoft.EntityFrameworkCore;
using Ripositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ripositories.EFCore
{
    public abstract class RepositoriesBase<T> : IRepositoryBase<T> where T : class
    {//abstract newlenemez alt kılasar _contex e erısım saglayabılsın dıye protected dıye tanımalrız
        protected readonly RepositoriesContext _context;
       
        public RepositoriesBase(RepositoriesContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Set<T>().Add(entity);

        public void Delete(T entity)=>_context.Set<T>().Remove(entity);

        public IQueryable<T> FinByCondition(Expression<Func<T, bool>> expresion, bool trackChanges = true) => !trackChanges ? _context.Set<T>().Where(expresion).AsNoTracking() : _context.Set<T>().Where(expresion);

        public IQueryable<T> FindByCondition(bool trackChanges = true) => !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
