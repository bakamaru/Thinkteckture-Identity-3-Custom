using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Logging;

namespace OpenIdentityServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
            //string about = RazorExtensions.RenderCustomView("about", "Home");
            //return Content(about);
        }
        public ActionResult Consent()
        {
            return View();
        }
        public ActionResult Logout()
        {

            var user = (ClaimsPrincipal)User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                var sub = user.GetSubjectId();
            }

            return View();
          
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Permissions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string id, string username, string password)
        {
            var env = Request.GetOwinContext().Environment;
            env.IssueLoginCookie(new IdentityServer3.Core.Models.AuthenticatedLogin
            {
                Subject = username,
                Name = username
            });
            var msg = env.GetSignInMessage(id);
            var returnUrl = msg.ReturnUrl;

            env.RemovePartialLoginCookie();

            return Redirect(returnUrl);
        }


    }
}