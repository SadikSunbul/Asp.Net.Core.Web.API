using Entities.DTO_DataTransferObject_;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilter;
using Services.Contrant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    //  [ApiVersion("1.0")]
    [ServiceFilter(typeof(LoFilterAttribute))]
    [ApiController]
    //[Route("api/{v:apiversion}/books")]
    [Route("api/books")]
    //[ResponseCache(CacheProfileName = "5mins")]
    [HttpCacheExpiration(CacheLocation=CacheLocation.Public,MaxAge =80)]
    public class BooksKontroller : ControllerBase
    {

        private readonly IServiceManager _manager;

        public BooksKontroller(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet(Name = "GetAllBooksAsync")]
        [ServiceFilter(typeof(ValidateMediaTypeAtribut))]
        [ResponseCache(Duration = 60)]//60 snlık bekleem ekledik
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters param)
        {
            var linkparameters = new LinkParameters()
            {
                BookParameters = param,
                HttpContext = HttpContext
            };

            var linkresult = await _manager.BookService.GetAllBooksAsync(linkparameters, false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(linkresult.metaData));

            return linkresult.linkRespons.HasLinks ?
                Ok(linkresult.linkRespons.LinkedEntities) :
                Ok(linkresult.linkRespons.ShapedEntites);


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

        [ServiceFilter(typeof(ValidationFilterAttribut))] //bunun la artık null kontrolu ve hata kontrolu ne gerek yoktur zaten ıcınde yazdık onları
        [HttpPost] //kaynak olustur
        public async Task<IActionResult> CreateOneBook([FromBody] BookDto book) //[FromBody] gelen requestin (gelen istek )badisinden alıcak veriyi
        {

            if (book == null)
            {
                return BadRequest(); //400 benım ıstedıgım turde degıl bu der
            }
            if (!ModelState.IsValid) //bunu atrubut tarafında kontrol edıcez artık buradan silcez
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

        [HttpOptions]
        public IActionResult GetBookOptions()
        {
            Response.Headers.Add("Allow", "GET,PUT,POST,PATCH,DELETE,HEAD,OPTIONS");
            return Ok();
        }

        [HttpHead] //get ile aynı işlevlere sahiptir head olanlarda request badisi olmaz
        public IActionResult a()
        {
            return Ok();
        }

    }
}
