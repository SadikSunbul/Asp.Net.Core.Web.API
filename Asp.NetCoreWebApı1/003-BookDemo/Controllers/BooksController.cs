using _004_BookDemo.Data;
using _004_BookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace _004_BookDemo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        #region Get (veri çekme )
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var data = ApplicationContext.Books.ToList();
            return Ok(data);
        }
        [HttpGet("{id:int}")] //"int/{id:int}" . :int Şablonun bölümü, yol değerlerini tamsayıya dönüştürülebilecek dizelerle kısıtlarid. için bir /api/test2/int/abcGET isteği:
                              // Bu eylemle eşleşmiyor.
        public IActionResult GetOneBooks([FromRoute(Name = "id")] int id) //[FromRoute(Name ="id")] --> linkden gelıcek adı id olanın degerini buraya ata dedik
        {

            var book = ApplicationContext
                .Books
                .Where(i => i.Id.Equals(id))
                .SingleOrDefault(); //tek bır kayıt ayda bos ıse null doner
            if (book is null)
            {
                return NotFound();//404
            }
            return Ok(book);
        }
        #endregion
        #region Post (kaynak olusturma)
        [HttpPost] //kaynak olustur
        public IActionResult CreateOneBook([FromBody]Book book) //[FromBody] gelen requestin (gelen istek )badisinden alıcak veriyi
        {
            try
            {
                if (book is null)
                {
                    return BadRequest(); //400 benım ıstedıgım turde degıl bu der
                }
                ApplicationContext.Books.Add(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Put (güncelleme)

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name ="id")]int id,[FromBody]Book book)
        {
            //chechk book?
            var entity=ApplicationContext
                .Books
                .Find(i => i.Id.Equals(id));
            if (entity is null)
            {
                return NotFound(); //404
            }

            //check id
            if (id!=book.Id)
            { //burada ıd ler uyusuyormu onu kontrol ettık bodıy ve route deki ıd ler 
                return BadRequest();//400
            }
            ApplicationContext.Books.Remove(entity); //sil bu veriyi
            book.Id = entity.Id; //her ıhtımale karsı eslıyoruz yapmasakta olur
            ApplicationContext.Books.Add(book); //ekledik burada
            //guncelleme tamamlanmıs olur
            return Ok(book);
        }

        #endregion
        #region Delete (silme)
        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent(); //204
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name ="id")]int id)
        {
            var entity=ApplicationContext
                .Books
                .Find(b=>b.Id.Equals(id));
            if (entity is null)
            {
                return NotFound(new
                {
                    statusCode=404,
                    message= $"Book with id:{id} could not found. "
                });
            }
            ApplicationContext.Books .Remove(entity);
            return NoContent();
        }

        #endregion
        #region Patch (kısmi güncelleme)
        //Microsoft.AspNetCore.Mvc.NewtonsoftJson
        //Microsoft.AspNetCore.JsonPatch 
        //eklenmesi gereken kütüphaneler
        //builder.Services.AddControllers().AddNewtonsoftJson(); yazılamlı programcs ye

        //frombody dekı verılerın JsonPatchDocument<Book>  ıle alınması gerekır

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name ="id")]int id,[FromBody] JsonPatchDocument<Book>  bookPatch)
        {
            var entıty = ApplicationContext.Books.Find(b => b.Id.Equals(id));
            if (entıty is null)
            {
                return NotFound();//404
            }

            
            bookPatch.ApplyTo(entıty); //entıtye bu degısıklıgı yansıt dedık
            return NoContent(); //204
        }


        #endregion
    }
}
