using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;
using Test.Application.ActiponFilter;
using Test.Application.Features.Commends._Product.CreateProduct;
using Test.Application.Features.Query._Product;

namespace Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName ="v1")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin,User")] //Admin veuser erişebilir
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommendRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException();
            }
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            var data = await mediator.Send(request);
            return Ok(data);
        }

        [Authorize(Roles ="User")] //koruma altına alır burası user olan buraya erıse bılır
        [HttpGet("/{PageNumber:int}/{PageSize:int}")]
        public async Task<IActionResult> GetAllProduct([FromRoute] GetAllProductQueryRequest request)
        {
            var data = await mediator.Send(request);

            Response.Headers.Add("X-Paginaton", JsonSerializer.Serialize(data.ayrıntı));

            return Ok(data.produks);
        }

    }
}
