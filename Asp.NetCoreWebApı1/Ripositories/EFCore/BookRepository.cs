using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Ripositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ripositories.EFCore
{
    public class BookRepository : RepositoriesBase<Book>, IBookRepository
    {
        public BookRepository(RepositoriesContext context) : base(context)
        {

        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);

        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool tracking = true)
        {
            var books = await FindAll(tracking)
                .OrderBy(b => b.Id)
                .ToListAsync();

            return PagedList<Book>.ToPagedLİst(books, bookParameters.PageNumber, bookParameters.PageSize);

        }


        public async Task<Book> GetOneBooksByIdAsync(int id, bool tracking = true) => await FinByCondition(B => B.Id.Equals(id), tracking).OrderBy(b => b.Id).SingleOrDefaultAsync();

        public void UpdateOneBook(Book book) => Update(book);
    }
}
