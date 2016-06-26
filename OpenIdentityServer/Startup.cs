using System.IdentityModel.Tokens;
using Autofac;
using IdentityServer3.AccessTokenValidation;

using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using Owin;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Logging;
using IdentityServer3.Core.Resources;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using OpenIdentityServer.Controllers;
using OpenIdentityServer.Services;
using OpenIdentityCustomService;
using OpenIdentityCustomService.AppClient;
using OpenIdentityCustomService.Configuration;
using OpenIdentityCustomService.ViewService;

[assembly: OwinStartup(typeof(OpenIdentityServer.Startup))]
namespace OpenIdentityServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {


            app.Map("/oauth", coreApp =>
            {
                var factory = new IdentityServerServiceFactory();
                factory.ClaimsProvider=new Registration<IClaimsProvider,CustomClaimProvider>();
                var clientStorageProvider = new ClientStorage();
                factory.ClientStore = new Registration<IClientStore>(resolver => clientStorageProvider);
                var scopeStorageProvider = new ScopeProvider();
                factory.ScopeStore = new Registration<IScopeStore>(resolver => scopeStorageProvider);

                //currently by defult supported for angular only
                factory.ViewService = new Registration<IViewService, CustomAngularViewService>();

                // different examples of custom user services
                //var userService = new RegisterFirstExternalRegistrationUserService();
                //var userService = new ExternalRegistrationUserService();
                var userService = new EulaAtLoginUserService();
                // var userService = new LocalRegistrationUserService();

                //for normal 
                // factory.UserService = new Registration<IUserService, EulaAtLoginUserService>();
                factory.UserService = new Registration<IUserService>(resolver => userService);

                //for custom login service
                // factory.UserService = new Registration<IUserService, LocalRegistrationUserService>();

                factory.Register(new Registration<TestController>(typeof (TestController)));

                var options = new IdentityServerOptions
                {
                    EnableWelcomePage = false,
                    //Endpoints = new EndpointOptions()
                    //{
                    //    EnableUserInfoEndpoint = true,
                    //    EnableTokenEndpoint = true,
                    //    EnableAuthorizeEndpoint = true,
                    //    EnableClientPermissionsEndpoint = true,
                    //    EnableTokenRevocationEndpoint = true,
                    //    EnableAccessTokenValidationEndpoint = true

                    //},
                    SiteName = "Identity Server",
                    SigningCertificate = Certificate.Get(),
                    Factory = factory,
                    ////this line for custom login
                    //AuthenticationOptions = new AuthenticationOptions
                    //{
                    //},

                    AuthenticationOptions = new AuthenticationOptions
                    {
                        RequireSignOutPrompt = false,

                        // IdentityProviders = ConfigureAdditionalIdentityProviders,
                        //LoginPageLinks = new LoginPageLink[] {
                        //    new LoginPageLink{
                        //        Text = "Register",
                        //        //Href = "~/localregistration"
                        //        Href = "localregistration"
                        //    }
                        //}
                    },

                    EventsOptions = new EventsOptions
                    {
                        RaiseSuccessEvents = true,
                        RaiseErrorEvents = true,
                        RaiseFailureEvents = true,
                        RaiseInformationEvents = true
                    }
                };

                coreApp.UseIdentityServer(options);

            });

            //JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            //app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            //{
            //    Authority = "https://localhost:44300/core",
                
            //    RequiredScopes = new[] { "openId","write","email","profile" },

            //    // client credentials for the introspection endpoint
            //    ClientId = "write",
            //    ClientSecret = "secret"
            //});

            //app.UseWebApi(WebApiConfig.Register());
        }

        public static void ConfigureAdditionalIdentityProviders(IAppBuilder app, string signInAsType)
        {
            var google = new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                SignInAsAuthenticationType = signInAsType,
                ClientId = "767400843187-8boio83mb57ruogr9af9ut09fkg56b27.apps.googleusercontent.com",
                ClientSecret = "5fWcBT0udKY7_b6E3gEiJlze"
            };
            app.UseGoogleAuthentication(google);

            var fb = new FacebookAuthenticationOptions
            {
                AuthenticationType = "Facebook",
                SignInAsAuthenticationType = signInAsType,
                AppId = "676607329068058",
                AppSecret = "9d6ab75f921942e61fb43a9b1fc25c63"
            };
            app.UseFacebookAuthentication(fb);

            var twitter = new TwitterAuthenticationOptions
            {
                AuthenticationType = "Twitter",
                SignInAsAuthenticationType = signInAsType,
                ConsumerKey = "N8r8w7PIepwtZZwtH066kMlmq",
                ConsumerSecret = "df15L2x6kNI50E4PYcHS0ImBQlcGIt6huET8gQN41VFpUCwNjM"
            };
            app.UseTwitterAuthentication(twitter);
        }
    }
}
