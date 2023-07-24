using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ripositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindByCondition(bool trackChanges=true);
        IQueryable<T> FinByCondition(Expression<Func<T,bool>> expresion,bool trackChanges=true);
        void Create(T entity);  
        void Update(T entity);  
        void Delete(T entity);  

    }
}
