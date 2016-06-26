using OpenIdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using OpenIdentityCustomService;

namespace OpenIdentityServer.Controllers
{
    public class EulaController : Controller
    {
        [Route("oauth/eula")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            // this verifies that we have a partial signin from idsvr
            var ctx = Request.GetOwinContext();
            var partial_user = await ctx.Environment.GetIdentityServerPartialLoginAsync();
            if (partial_user == null)
            {
                return View("Error");
            }
            
            return View();
        }

        [Route("oauth/eula")]
        [HttpPost]
        public async Task<ActionResult> Index(string button)
        {
            var ctx = Request.GetOwinContext();
            var partial_user = await ctx.Environment.GetIdentityServerPartialLoginAsync();
            if (partial_user == null)
            {
                return View("Error");
            }

            if (button == "yes")
            {
                // update the "database" for our users with the outcome
                var subject = partial_user.GetSubjectId();
                //  var user = EulaAtLoginUserService.Users.Single(x => x.Subject == subject);
                EulaAtLoginUserService.AcceptedEula = true;

                // find the URL to continue with the process to the issue the token to the RP
                var resumeUrl = await ctx.Environment.GetPartialLoginResumeUrlAsync();
                return Redirect(resumeUrl);
            }

            ViewBag.Message = "Well, until you accept you can't continue.";
            return View();
        }
    }
}
