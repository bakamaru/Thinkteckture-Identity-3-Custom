using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityModel.Client;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace DirectAccessApp.Controllers
{
    public class ManualCodeFlowDemoController : Controller
    {
        // GET: ManualCodeFlowDemo
        public ActionResult Index()
        {
            Request.GetOwinContext().Authentication.SignOut("Cookies");

            return View();
        }

        [HttpPost]
        public ActionResult Index(string scopes)
        {
           ManualFlowClient client=new ManualFlowClient();
            return Redirect(client.Connect(scopes));
        }

        public async Task<ActionResult> Callback()
        {
            ViewBag.Code = Request.QueryString["code"] ?? "none";

            var state = Request.QueryString["state"];
            ManualFlowClient client = new ManualFlowClient();
            var tempState = await client.GetTempStateAsync();

            if (state.Equals(tempState.Item1, StringComparison.Ordinal))
            {
                ViewBag.State = state + " (valid)";
            }
            else
            {
                ViewBag.State = state + " (invalid)";
            }

            ViewBag.Error = Request.QueryString["error"] ?? "none";

            return View();
        }

        [HttpPost]
        [ActionName("callback")]
        public async Task<ActionResult> GetToken()
        {
            ManualFlowClient client = new ManualFlowClient();
            var response = await client.GetToken();
            return View("Token", response);
        }

        public ActionResult AppIndex()
        {
            return View();
        }

        public async Task<ActionResult> CallService()
        {
            ManualFlowClient client = new ManualFlowClient();

            var result = await client.CallService("");

            return View(result);
        }

        public async Task<ActionResult> RefreshToken()
        {
            ManualFlowClient client = new ManualFlowClient();

            await client.RefreshToken();
            return RedirectToAction("AppIndex");
        }

        public async Task<ActionResult> RevokeAccessToken()
        {

            ManualFlowClient client = new ManualFlowClient();

            await client.RevokeAccessToken();

            return RedirectToAction("AppIndex");
        }

        public async Task<ActionResult> RevokeRefreshToken()
        {
            ManualFlowClient client = new ManualFlowClient();

            await client.RevokeRefreshToken();

            return RedirectToAction("AppIndex");
        }
    }
}