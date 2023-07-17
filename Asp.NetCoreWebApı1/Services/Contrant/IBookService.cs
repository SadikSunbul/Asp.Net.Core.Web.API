using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contrant
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks(bool tracking=true);
        Book GetOneBookById(int id,bool tracking=true);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id, Book book);
        void DeleteOneBook(int id,bool tracking=true);
    }
}
