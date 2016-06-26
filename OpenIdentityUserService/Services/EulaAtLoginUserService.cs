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
using OpenIdentityCustomService.Services;
using OpenIdentityCustomService.User;

namespace OpenIdentityCustomService
{
    public class EulaAtLoginUserService : UserServiceBase
    {

        //public class CustomUser
        //{
        //    public string Subject { get; set; }
        //    public string Username { get; set; }
        //    public string Password { get; set; }
        //    public bool AcceptedEula { get; set; }
        //    public List<Claim> Claims { get; set; }
        //}

        private readonly SfUserService _userService = new SfUserService();
        //public static List<EulaUser> Users = new List<EulaUser>()
        //{
        //    new EulaUser{
        //        Subject = "123",
        //        Username = "alice",
        //        Password = "alice",
        //        AcceptedEula = false,
        //        Claims = new List<Claim>{
        //            new Claim(Constants.ClaimTypes.GivenName, "Alice"),
        //            new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
        //            new Claim(Constants.ClaimTypes.Email, "AliceSmith@email.com"),
        //        }
        //    },
        //    new EulaUser{
        //        Subject = "890",
        //        Username = "bob",
        //        Password = "bob",
        //        AcceptedEula = false,
        //        Claims = new List<Claim>{
        //            new Claim(Constants.ClaimTypes.GivenName, "Bob"),
        //            new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
        //            new Claim(Constants.ClaimTypes.Email, "BobSmith@email.com"),
        //        }
        //    },
        //};

        public static bool AcceptedEula = false;


        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            SfUser user = null;
            // var user = Users.SingleOrDefault(x => x.Username == context.UserName && x.Password == context.Password);
            //for special url based token generation
            if (context.SignInMessage.AcrValues.SingleOrDefault(x => x.Contains("dpa"))!=null)
                user= _userService.IsValidUser(context.UserName);
            else
             user = _userService.IsValidUser(context.UserName, context.Password);
            if (user != null)
            {
                var skipeula = context.SignInMessage.AcrValues.SingleOrDefault(x => x.Contains("SkipEula"));
                if (skipeula == null)
                {
                    if (AcceptedEula)
                    {
                        context.AuthenticateResult = new AuthenticateResult(user.Subject, user.UserName);
                        AcceptedEula = false;
                    }
                    else
                    {
                        context.AuthenticateResult = new AuthenticateResult("~/eula", user.Subject, user.UserName);
                    }
                }
                else
                {
                    var userdetail = _userService.GetProfile(context.UserName);
                    var claims = AppClaims.GenerateDefaultClaims(userdetail);

                    context.AuthenticateResult = new AuthenticateResult(user.Subject, user.UserName, claims);
                    AcceptedEula = false;
                }
            }

            return Task.FromResult(0);
        }

        public override Task PreAuthenticateAsync(PreAuthenticationContext context)
        {

            return base.PreAuthenticateAsync(context);
        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            // issue the claims for the user
            // user = Users.SingleOrDefault(x => x.Subject == context.Subject.GetSubjectId());
            var user = _userService.GetProfile(context.Subject.GetName());
            if (user != null)
            {

                ////TODO:: issue claim
                //user.Claims = new List<Claim>();
                //user.Claims.Add(new Claim(Constants.ClaimTypes.Gender, user.Gender ?? ""));
                //user.Claims.Add(new Claim(Constants.ClaimTypes.BirthDate, user.DateOfBirth == null ? "" : user.DateOfBirth.ToString()));
                //user.Claims.Add(new Claim(Constants.ClaimTypes.Name, user.FirstName ?? ""));
                //user.Claims.Add(new Claim(Constants.ClaimTypes.FamilyName, user.LastName ?? ""));

                //if (context.RequestedClaimTypes.Contains("email"))
                //{
                //    user.Claims.Add(new Claim(Constants.ClaimTypes.Email, user.Email ?? ""));
                //}
                //user.Claims.Add(new Claim("res_phone", user.ResPhone ?? ""));
                //user.Claims.Add(new Claim(Constants.ClaimTypes.PhoneNumber, user.Mobile ?? ""));

                //user.Claims.Add(new Claim(Constants.ClaimTypes.Address, user.StreetAddress1 ?? ""));
                //user.Claims.Add(new Claim("address2", user.StreetAddress2 ?? ""));
                //user.Claims.Add(new Claim("city", user.City ?? ""));
                //user.Claims.Add(new Claim("country", user.CountryCode ?? ""));

                //// user.Claims.Add(new Claim(ClaimTypes.PostalCode, user.PostalCode ?? ""));
                ////user.Claims.Add(new Claim("gender22", "male"));
                ////user.Claims.Add(new Claim("gender2", "male"));
                ////user.Claims.Add(new Claim("gender3", "male"));
                var claims = AppClaims.GenerateDefaultClaims(user);
                if (context.AllClaimsRequested)
                    context.IssuedClaims = claims;
                else
                    context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type));
            }

            return Task.FromResult(0);
        }
    }
}
