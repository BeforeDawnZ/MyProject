using MyWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Controllers
{
    public class BasicAuthenticationHandler : DelegatingHandler
    {
        private const string authenticationHeader = "WWW-Authenticate";
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var crendentials = ParseHeader(request);

            if (crendentials != null)
            {
                var identity = new BasicAuthenticationIdentity(crendentials.Name, crendentials.Password);

                var principal = new GenericPrincipal(identity, null);

                Thread.CurrentPrincipal = principal;

                //针对于ASP.NET设置
                //if (HttpContext.Current != null)
                //    HttpContext.Current.User = principal;
            }

            return base.SendAsync(request, cancellationToken).ContinueWith(task => {
                var response = task.Result;
                if (crendentials == null && response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Challenge(request, response);
                }

                return response;
            });



        }

        void Challenge(HttpRequestMessage request, HttpResponseMessage response)
        {
            var host = request.RequestUri.DnsSafeHost;

            response.Headers.Add(authenticationHeader, string.Format("Basic realm=\"{0}\"", host));

        }

        public virtual BasicAuthenticationIdentity ParseHeader(HttpRequestMessage requestMessage)
        {
            string authParameter = null;

            var authValue = requestMessage.Headers.Authorization;
            if (authValue != null && authValue.Scheme == "Basic")
                authParameter = authValue.Parameter;

            if (string.IsNullOrEmpty(authParameter))

                return null;

            authParameter = Encoding.Default.GetString(Convert.FromBase64String(authParameter));

            var authToken = authParameter.Split(':');
            if (authToken.Length < 2)
                return null;

            return new BasicAuthenticationIdentity(authToken[0], authToken[1]);
        }
    }
}