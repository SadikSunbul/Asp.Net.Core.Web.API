using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Application.ActiponFilter
{
    public class IsValidationFilter : ActionFilterAttribute
    {
        public IsValidationFilter()
        {
            
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            var param = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains("Request")).Value;

            if (param == null)
            {
                context.Result = new BadRequestObjectResult($"object is null controller:{controller} action:{action}");
                return;
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
