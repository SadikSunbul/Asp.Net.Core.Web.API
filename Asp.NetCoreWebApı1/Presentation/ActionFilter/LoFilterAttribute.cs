using Entities.LogModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Services.Contrant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilter
{
    public class LoFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogerService logerService;

        public LoFilterAttribute(ILogerService logerService)
        {
            this.logerService = logerService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            logerService.LogInfo(Log("OnActionExecuting", context.RouteData));
        }

        private string Log(string modelname, RouteData routeData)
        {
            var logDetails = new LogDetails()
            {
                ModelName = modelname,
                Controller = routeData.Values["controller"],
                Action = routeData.Values["action"]
            };
            if(routeData.Values.Count>=3) //ıd var demek
            {
                logDetails.Id = routeData.Values["id"];
            }
            return logDetails.ToString();
        }
    }
}
