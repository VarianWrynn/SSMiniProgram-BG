using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web;

namespace SSMiniProgram.Filters
{
    /// <summary>
    /// https://www.cnblogs.com/zhaoshujie/p/9765268.html
    /// 在WEB Api中，引入了面向切面编程（AOP）的思想，在某些特定的位置可以插入特定的
    /// Filter进行过程拦截处理。引入了这一机制可以更好地践行DRY(Don’t Repeat Yourself)思想，
    /// 通过Filter能统一地对一些通用逻辑进行处理，如：权限校验、参数加解密、参数校验等方面
    /// 我们都可以利用这一特性进行统一处理，今天我们来介绍Filter的开发、使用以及讨论他们的执行顺序。
    /// </summary>
    public class AuthFilter: AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            //HttpContextBase abstractContext = new System.Web.HttpContextWrapper(context);

            //url获取token 
            var content = actionContext.Request.Properties["MS_HttpContext"];
        }
    }
}
