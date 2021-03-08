
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
//using Microsoft.AspNetCore.Mvc.Filters;


/*
 * System.Web.Http.Filters;  &  Microsoft.AspNetCore.Mvc.Filters; 都存在 
 * ExceptionFilterAttribute （classs)
 * IExceptionFilter (interface)
 * 
| Filter 类型   | 实现的接口           | 描述                                 |
|---------------|----------------------|--------------------------------------|
| Authorization | IAuthorizationFilter | 最先运行的Filter，被用作请求权限校验 |
|     Action    |     IActionFilter    | 在Action运行的前、后运行             |
|   Exception   |   IExceptionFilter   | 当异常发生的时候运行                 |

 */

namespace SSMiniProgram.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            _ = new HttpResponseMessage(HttpStatusCode.InternalServerError).StatusCode;//设置错误代码：例如：500 404
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            string msg = JsonConvert.SerializeObject(new
            {
                success = false,
                message = actionExecutedContext.Exception.Message
            });

            actionExecutedContext.Response.Content = new StringContent(msg, Encoding.UTF8);
        }
    }
}
