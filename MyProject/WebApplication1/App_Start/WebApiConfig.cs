using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication1.Controllers;

namespace MyWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            //  config.Filters.Add(new Filters.ApiSecurityFilter());
            // config.MessageHandlers.Add(new BasicAuthenticationHandler());
            // config.Filters.Add(new BasicAuthenticationFilter());
            config.Filters.Add(new AuthFilterAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
