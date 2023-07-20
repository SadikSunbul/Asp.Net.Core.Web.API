using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contrant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksKontroller:ControllerBase
    {

        private readonly IServiceManager _manager;

        public BooksKontroller(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBoks()
        {
            
                var books = _manager.BookService.GetAllBooks(false);
                return Ok(books);
            
            
        }

        #region Get (veri çekme )

        [HttpGet("{id:int}")] //"int/{id:int}" . :int Şablonun bölümü, yol değerlerini tamsayıya dönüştürülebilecek dizelerle kısıtlarid. için bir /api/test2/int/abcGET isteği:
                              // Bu eylemle eşleşmiyor.
        public IActionResult GetOneBooks([FromRoute(Name = "id")] int id) //[FromRoute(Name ="id")] --> linkden gelıcek adı id olanın degerini buraya ata dedik
        {

            var book = _manager
                .BookService
                .GetOneBookById(id, false); //tek bır kayıt ayda bos ıse null doner
            if (book is null)
            {
                throw new BookNotFound(id); //kendi hatamızı kullandık 
            }
            return Ok(book);
        }
        #endregion
        #region Post (kaynak olusturma)
        [HttpPost] //kaynak olustur
        public async Task<IActionResult> CreateOneBook([FromBody] Book book) //[FromBody] gelen requestin (gelen istek )badisinden alıcak veriyi
        {

            if (book == null)
            {
                return BadRequest(); //400 benım ıstedıgım turde degıl bu der
            }
            _manager.BookService.CreateOneBook(book);
            return StatusCode(201, book);
        }
        #endregion
        #region Put (güncelleme)

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            //chechk book?
            _manager.BookService.UpdateOneBook(id, book);
            return NoContent();
        }

        #endregion
        #region Delete (silme)

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            _manager.BookService.DeleteOneBook(id);
            return NoContent();
        }

        #endregion

    }
}
