using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartupAttribute(typeof(DirectAccessApp.Startup))]
namespace DirectAccessApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);

            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "TempCookie",
                AuthenticationMode = AuthenticationMode.Passive
            });
        }
    }
}
