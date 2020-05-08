using MyWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApplication1.Controllers
{
    public class BasicAuthenticationFilter : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            var identity = Thread.CurrentPrincipal.Identity;
            //if (identity != null && HttpContext.Current != null)
            //    identity = HttpContext.Current.User.Identity;

            //if (identity != null && identity.IsAuthenticated)
            if (identity != null)
            {

                var basicAuthIdentity = identity as BasicAuthenticationIdentity;

                //可以添加其他需要的业务逻辑验证代码
                if (basicAuthIdentity.Name == "MyProject" && basicAuthIdentity.Password == "123456")
                {
                    return true;
                }
            }

            return false;

        }
    }
}