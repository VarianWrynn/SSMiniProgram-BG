using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SSMiniProgram.Filters
{
    /*
    * http://stackoverflow.com/questions/12606202/system-web-mvc-actionfilterattribute-vs-system-web-http-filters-actionfilterattr
    * 
    * System.Web.Mvc.ActionFilterAttribute and System.Web.Http.Filters.ActionFilterAttribute
    * what is different?
    * 
    * 
    * The System.Web.Http one is for Web API; the System.Web.Mvc one is for previous MVC versions.
    * You can see from the source that the Web API version has several differences.
    * It has OnResultExecuting and OnResultExecuted handlers ("Called by the ASP.NET MVC framework before/after the action result executes.")
    * It can be executed asynchronously
    * It does not let you specify an order of execution
    * 
    */

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class ActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            return;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            // log operation here
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
