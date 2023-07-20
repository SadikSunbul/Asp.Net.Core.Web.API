using Entities.DTO_DataTransferObject_;
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
        IEnumerable<BookDto> GetAllBooks(bool tracking = true);
        BookDto GetOneBookById(int id, bool tracking = true);
        BookDto CreateOneBook(BookDto book);
        void UpdateOneBook(int id, BookDTOForUpdate book);
        void DeleteOneBook(int id, bool tracking = true);
    }
}
