using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace ClientWebApp.Controllers
{
    public class HomeController : Controller
    {
        string apiurl = "http://localhost:2727/";
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Claims()
        {
            ViewBag.Message = "Claims";

            var user = User as ClaimsPrincipal;
            var token = user.FindFirst("access_token");

            if (token != null)
            {
                ViewData["access_token"] = token.Value;
            }

            return View();
        }

        public async Task<ActionResult> CallApi()
        {
            var token = (User as ClaimsPrincipal).FindFirst("access_token").Value;
            var client = new HttpClient();
            client.SetBearerToken(token);

            var result = await client.GetStringAsync(apiurl + "identity");
            ViewBag.Json = JArray.Parse(result.ToString());

            return View();
        }


        public ActionResult Signout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        public void SignoutCleanup(string sid)
        {
            var cp = (ClaimsPrincipal)User;
            var sidClaim = cp.FindFirst("sid");
            if (sidClaim != null && sidClaim.Value == sid)
            {
                Request.GetOwinContext().Authentication.SignOut("Cookies");
            }
        }
    }
}