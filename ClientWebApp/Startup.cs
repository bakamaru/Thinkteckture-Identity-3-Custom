using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
[assembly: OwinStartup(typeof(ClientWebApp.Startup))]

namespace ClientWebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "mvc.owin.implicit",
                Authority = "https://localhost:44300/core", //identity server url
                RedirectUri = "https://localhost:44301/",
                ResponseType = "id_token",
                Scope = "openid email",

                UseTokenLifetime = false,
                SignInAsAuthenticationType = "Cookies"
            });
        }
    }
}
