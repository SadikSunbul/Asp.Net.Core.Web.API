using _002_BoşAsp.Net.Models;
using Microsoft.AspNetCore.Mvc;

namespace _002_BoşAsp.Net.Controllers
{
    [ApiController] //apı yapısını desteklesın dıye verdık hazır defeaul hata sayfalar gıbı yapuları bu sınıfta kullanmak ıcın kullanılır
    [Route("home")] // ..../home  demke lazım buraya erısmek ıcın 
    public class HomeController: ControllerBase //ControllerBase bunu controller ozelıklerını kazansın dıye ekledik
    {
        [HttpGet]
        public IActionResult GetMessage()
        {
            var result= new ResponseModel
            {
                HttpStatus = 200,
                Message = "Hello asp.net core web api"
            };
            return Ok(result);
        }

    }
}
