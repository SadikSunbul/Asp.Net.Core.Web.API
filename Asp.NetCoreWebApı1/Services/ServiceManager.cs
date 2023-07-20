using Ripositories.Contracts;
using Services.Contrant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        public ServiceManager(IRepositoryManager repositoryManager,ILogerService log)
        {
            _bookService = new Lazy<IBookService>(() => new BookManager(repositoryManager, log));
        }
        public IBookService BookService => _bookService.Value;
    }
}
