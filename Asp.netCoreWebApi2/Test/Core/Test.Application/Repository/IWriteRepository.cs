using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entites.Common;

namespace Test.Application.Repository
{
    public interface IWriteRepository<T>:IRepository<T> where T : BaseEntiey
    {
        Task<bool> AddAsync(T model); //sonucu true veya false doncem 
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(string id); //id sını verdıgımıde sılsın
        bool Update(T model);

        Task<int> SaveAsync();
    }
}
