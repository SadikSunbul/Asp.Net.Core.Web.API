using Microsoft.AspNetCore.Mvc;
using Services.Contrant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
  //  [ApiVersion("2.0",Deprecated = true)] //buradakı destegı kaldırdık demek
    [ApiController]
    // [Route("api/{v:apiversion}/books")] //urlye versıon bılgısı tasıdık
    [Route("api/books")] //Header ıle kullanmak ıcın 
    public class BooksV2Controller : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksV2Controller(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _manager.BookService.GetAllBooksAsync(false);
            var booksV2 = books.Select(b => new
            {
                Title = b.Title,
                Id = b.Id
            });
            return Ok(booksV2);
        }
    }
}
