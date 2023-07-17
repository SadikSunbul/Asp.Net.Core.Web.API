using _003_ProductApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _003_ProductApp.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Dependency injection

        private readonly ILogger<ProductsController> logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            this.logger = logger;
        }

        #endregion


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                 ProductName = "computer"
                },
                new Product
                {
                    Id = 2,
                 ProductName = "keybord"
                },
                new Product
                {
                    Id = 3,
                 ProductName = "Mause"
                }
            };
            logger.LogInformation("Get all products action çagrıldı"); //ınfo sevıyesınde log tutuk bunu consola yazdı
            return Ok(products);
        }
        [HttpPost]
        public IActionResult GetAllProducts([FromBody]Product product) //requestın badısınden gelın dedık [FromBody]
        {
            logger.LogWarning("product kaynak olusturuldu");
            return StatusCode(201);//created.
        }
    }
}
