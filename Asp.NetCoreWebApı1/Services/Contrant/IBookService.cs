using Entities.DTO_DataTransferObject_;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contrant
{
    public interface IBookService
    {
        Task<(IEnumerable<ExpandoObject> books, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters, bool tracking = true);
        Task<BookDto> GetOneBookByIdAsync(int id, bool tracking = true);
        Task<BookDto> CreateOneBookAsync(BookDto book);
        Task UpdateOneBookAsync(int id, BookDTOForUpdate book);
        Task DeleteOneBookAsync(int id, bool tracking = true);
    }
}
