using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Application.ActiponFilter;
using Test.Application.Features.Commends._User.CreateUser;
using Test.Application.Features.Query._User;

namespace Test.Api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [ApiExplorerSettings(GroupName ="v2")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IMediator mediator;

        public AuthenticationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [ServiceFilter(typeof(IsValidationFilter))]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommendRequest request)
        {
            var result = await mediator.Send(request);
            if (!result.result.Succeeded)
            {//basarısız ıse 
                foreach (var error in result.result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);//Basarılı
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(IsValidationFilter))]
        public async Task<IActionResult> UserLogin([FromBody] UserForAuthenticationQueryRequest request)
        {
            var respons = await mediator.Send(request);
            if (respons.Success == false)
            {
                return Unauthorized();
            }
            return Ok(new
            {
                Token = respons.Token
            });
        }
    }
}
