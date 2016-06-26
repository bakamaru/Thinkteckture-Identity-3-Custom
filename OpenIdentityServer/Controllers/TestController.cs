using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.Default;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace OpenIdentityServer.Controllers
{
   
    public class TestController : ApiController
    {
      
        public dynamic Get()
        {
            //var principal = User as ClaimsPrincipal;

            //return from c in principal.Identities.First().Claims
            //       select new
            //       {
            //           c.Type,
            //           c.Value
            //  };
            try
            {

             var a1=  (IOwinContext) HttpContext.Current.GetOwinContext();
                var a2 = (IOwinContext)HttpContext.Current.GetOwinContext();
                var z2=a2.Environment.ResolveDependency<IUserService>();
                var asdf =
                ((Microsoft.Owin.OwinRequest)
                    ((Microsoft.Owin.OwinContext) (HttpContext.Current.GetOwinContext())).Request).Context
                    .Get<ILifetimeScope>("idsrv:autofac:OwinLifetimeScope");
           var z= HttpContext.Current.GetOwinContext().Get<ITokenService>("idsvr:autofac:OwinLifetimeScope");
            var x= HttpContext.Current.GetOwinContext().Get<ILifetimeScope>("idsvr:autofac:OwinLifetimeScope");
            var y= Request.GetOwinContext().Environment.ResolveDependency<IUserService>();
            var requestScope= ControllerContext.Request.GetDependencyScope();
          var service=  requestScope.GetService(typeof (ITokenService)) as ITokenService;
            var tokenService=   DependencyResolver.Current.GetService<ITokenService>();
            HttpContext.Current.GetOwinContext();
            
            return "";
                //  var idToken = await _tokenService.CreateIdentityTokenAsync(tokenRequest);
                // var jwt = await _tokenService.CreateSecurityTokenAsync(idToken);
                // response.IdentityToken = jwt;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static async Task<string> IssueClientToken(IDictionary<string, object> env, string clientId, string scope, int lifetime)
        {
            var signingService = env.ResolveDependency<ITokenSigningService>();
            var issuerUri = env.GetIdentityServerIssuerUri();

            var token = new Token
            {
                Issuer = issuerUri,
                Audience = issuerUri + "/resources",
                Lifetime = lifetime,
                Claims = new List<Claim>
                {
                    new Claim("client_id", clientId),
                    new Claim("scope", scope)
                }
            };

            return await signingService.SignTokenAsync(token);
        }
    }
}
