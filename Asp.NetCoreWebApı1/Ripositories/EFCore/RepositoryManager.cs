using Ripositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ripositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoriesContext _context;
        private readonly Lazy<IBookRepository> _bookrepository;

        public RepositoryManager(RepositoriesContext context)
        {
            _context = context;
            _bookrepository = new Lazy<IBookRepository>(() => new BookRepository(_context));
        }

        public IBookRepository Book => _bookrepository.Value;

        public void Save() => _context.SaveChanges();
    }
}
