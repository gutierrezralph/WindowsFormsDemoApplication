using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Task.Web.Api.Infrastructure.Filter
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
                actionContext.Response = 
                    actionContext.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, actionContext.ModelState);
        }
    }
}