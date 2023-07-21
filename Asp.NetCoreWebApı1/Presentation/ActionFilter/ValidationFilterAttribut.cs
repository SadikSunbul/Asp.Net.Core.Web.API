using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilter
{
    public class ValidationFilterAttribut : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];//Hangi kontroller calısıyor ogrenıcez
            var action = context.RouteData.Values["action"];//Hangı actıon calısıyor ogrenıcez

            //Dto
            var param = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value; //dto yakaladık burada bunun degerını aldık

            if (param == null)
            {
                context.Result = new BadRequestObjectResult($"Object is null Controller:{controller} Action:{action}");
                return;
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
