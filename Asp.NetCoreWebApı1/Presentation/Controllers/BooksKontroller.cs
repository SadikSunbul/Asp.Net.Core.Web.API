using Entities.DTO_DataTransferObject_;
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
    public class BooksKontroller : ControllerBase
    {

        private readonly IServiceManager _manager;

        public BooksKontroller(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBoks()
        {

            var books = await _manager.BookService.GetAllBooksAsync(false);
            return Ok(books);


        }

        #region Get (veri çekme )

        [HttpGet("{id:int}")] //"int/{id:int}" . :int Şablonun bölümü, yol değerlerini tamsayıya dönüştürülebilecek dizelerle kısıtlarid. için bir /api/test2/int/abcGET isteği:
                              // Bu eylemle eşleşmiyor.
        public async Task<IActionResult> GetOneBooks([FromRoute(Name = "id")] int id) //[FromRoute(Name ="id")] --> linkden gelıcek adı id olanın degerini buraya ata dedik
        {

            var book = await _manager
                .BookService
                .GetOneBookByIdAsync(id, false); //tek bır kayıt ayda bos ıse null doner
            if (book is null)
            {
                throw new BookNotFound(id); //kendi hatamızı kullandık 
            }
            return Ok(book);
        }
        #endregion
        #region Post (kaynak olusturma)
        [HttpPost] //kaynak olustur
        public async Task<IActionResult> CreateOneBook([FromBody] BookDto book) //[FromBody] gelen requestin (gelen istek )badisinden alıcak veriyi
        {

            if (book == null)
            {
                return BadRequest(); //400 benım ıstedıgım turde degıl bu der
            }
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            await _manager.BookService.CreateOneBookAsync(book);
            return StatusCode(201, book);
        }
        #endregion
        #region Put (güncelleme)

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookDTOForUpdate book)
        {
            //chechk book?
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            await _manager.BookService.UpdateOneBookAsync(id, book);
            return NoContent();
        }

        #endregion
        #region Delete (silme)

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id);
            return NoContent();
        }

        #endregion

    }
}
