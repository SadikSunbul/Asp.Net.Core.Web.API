using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilter
{
    public class ValidateMediaTypeAtribut : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var acceptHeaderPresent = context.HttpContext
                .Request
                .Headers
                .ContainsKey("Accept");
            if (!acceptHeaderPresent)
            {
                context.Result = new BadRequestObjectResult($"Accept header is missing!");
                return;
            }
            var madiaType = context.HttpContext
                .Request
                .Headers["Accept"]
                .FirstOrDefault();

            if (!MediaTypeHeaderValue.TryParse(madiaType, out MediaTypeHeaderValue? outMediaType))//microsoftan cöz
            {
                context.Result = new BadRequestObjectResult($"Media type not present." +
                    $"Please add Accept header with request media type");
                return;
            }
            context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
        }
    }
}
