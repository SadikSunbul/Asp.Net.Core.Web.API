using Entities.Models;
using Ripositories.Contracts;
using Services.Contrant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILogerService _logerService;

        public BookManager(IRepositoryManager manager, ILogerService logerService)
        {
            _manager = manager;
            _logerService = logerService;
        }



        public Book CreateOneBook(Book book)
        {
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool tracking = true)
        {
            //check entıtıy
            var entıty = _manager.Book.GetOneBooksById(id, tracking);
            if (entıty is null)
            {
                string message= $"The book with id:{id} colf not found";
                _logerService.LogInfo(message);
                throw new Exception(message);
            }
            _manager.Book.DeleteOneBook(entıty);
            _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool tracking = true)
        {
            return _manager.Book.GetAllBooks();
        }

        public Book GetOneBookById(int id, bool tracking = true)
        {
            return _manager.Book.GetOneBooksById(id, tracking);
        }

        public void UpdateOneBook(int id, Book book)
        {
            var entity = _manager.Book.GetOneBooksById(id);
            if (entity is null)
            {
                throw new Exception($"book whit id: {id} could not found.");
            }

            //check parms
            if (book is null)
            {
                throw new ArgumentException(nameof(book));

            }
            entity.Title = book.Title;
            entity.Price = book.Price;

            _manager.Book.Update(entity);
            _manager.Save();

        }
    }
}
