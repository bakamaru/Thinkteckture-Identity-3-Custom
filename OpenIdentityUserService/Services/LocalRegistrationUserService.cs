using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.Owin;
using OpenIdentityCustomService.User;

namespace OpenIdentityCustomService
{
    public class LocalRegistrationUserService : UserServiceBase
    {
        //public class CustomUser
        //{
        //    public string Subject { get; set; }
        //    public string Username { get; set; }
        //    public string Password { get; set; }
        //    public List<Claim> Claims { get; set; }
        //}

        public static List<LocalUser> Users = new List<LocalUser>()
        {
            new LocalUser()
            {
                Username = "binod",
                Password = "binod",
                Subject = "test",
                Claims = new List<Claim>()
                {
                    new Claim("role","user"),
                    new Claim("general","test")
                }

            }
        };
        #region for custom login

        //OwinContext ctx;
        //public LocalRegistrationUserService(OwinEnvironmentService owinEnv)
        //{
        //    ctx = new OwinContext(owinEnv.Environment);
        //}

        //public override Task PreAuthenticateAsync(PreAuthenticationContext context)
        //{
        //    var id = ctx.Request.Query.Get("signin");
        //    context.AuthenticateResult = new AuthenticateResult("~/login?id=" + id, (IEnumerable<Claim>)null);
        //    return Task.FromResult(0);
        //}

        #endregion
        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var user = Users.SingleOrDefault(x => x.Username == context.UserName && x.Password == context.Password);
            if (user != null)
            {
                context.AuthenticateResult = new AuthenticateResult(user.Subject, user.Username);
            }

            return Task.FromResult(0);
        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            // issue the claims for the user
            var user = Users.SingleOrDefault(x => x.Subject == context.Subject.GetSubjectId());
            if (user != null)
            {
                context.IssuedClaims = user.Claims.Where(x => context.RequestedClaimTypes.Contains(x.Type));
            }

            return Task.FromResult(0);
        }
    }
}
