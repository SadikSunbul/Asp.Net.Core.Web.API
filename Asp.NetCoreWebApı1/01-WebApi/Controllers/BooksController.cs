using _01_WebApi.Models;
using _01_WebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace _01_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContex _context;

        public BooksController(RepositoryContex context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBoks()
        {
            try
            {
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Get (veri çekme )

        [HttpGet("{id:int}")] //"int/{id:int}" . :int Şablonun bölümü, yol değerlerini tamsayıya dönüştürülebilecek dizelerle kısıtlarid. için bir /api/test2/int/abcGET isteği:
                              // Bu eylemle eşleşmiyor.
        public IActionResult GetOneBooks([FromRoute(Name = "id")] int id) //[FromRoute(Name ="id")] --> linkden gelıcek adı id olanın degerini buraya ata dedik
        {

            var book = _context
                .Books
                .FirstOrDefault(i => i.Id.Equals(id)); //tek bır kayıt ayda bos ıse null doner
            if (book is null)
            {
                return NotFound();//404
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
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return StatusCode(201, book);
        }
        #endregion
        #region Put (güncelleme)

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            //chechk book?
            var entity = _context
                .Books.Where(i => i.Id.Equals(id)).SingleOrDefault();
            if (entity is null)
            {
                return NotFound(); //404
            }

            //check id
            if (id != book.Id)
            { //burada ıd ler uyusuyormu onu kontrol ettık bodıy ve route deki ıd ler 
                return BadRequest();//400
            }

            entity.Title = book.Title;
            entity.Price = book.Price;

            _context.SaveChanges();
            //guncelleme tamamlanmıs olur
            return Ok(book);
        }

        #endregion
        #region Delete (silme)

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            var entity = _context
                .Books
                .Where(b => b.Id.Equals(id)).SingleOrDefault();
            if (entity is null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Book with id:{id} could not found. "
                });
            }
            _context.Books.Remove(entity);
            _context.SaveChanges();
            return NoContent();
        }

        #endregion




    }
}
