using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OpenIdentityCustomService
{

    public class RazorExtensions
    {
        public static string RenderCustomView(string viewName, string controller, object model = null)
        {
            try
            {


                using (var sw = new StringWriter())
                {
                    // var cContext = ControllerContext;
                    //this is for adding route
                    var context = new HttpContextWrapper(System.Web.HttpContext.Current);
                    var viewData = new ViewDataDictionary();
                    var tempData = new TempDataDictionary();
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", controller);
                    routeData.Values.Add("action", "index");
                    ControllerContext cContext = new ControllerContext { RouteData = routeData, Controller = new FakeController(), HttpContext = context };
                    if (model != null)
                        viewData.Model = model;
                    var viewResult = ViewEngines.Engines.FindPartialView(cContext, viewName);
                    var viewContext = new ViewContext(cContext, viewResult.View, viewData, tempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(cContext, viewResult.View);
                    return sw.GetStringBuilder().ToString();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public static string RenderCustomView(string viewName, string controller, ViewDataDictionary viewData, TempDataDictionary tempData, object model = null)
        {

            using (var sw = new StringWriter())
            {
                // var cContext = ControllerContext;
                //this is for adding route
                var context = new HttpContextWrapper(System.Web.HttpContext.Current);
                var routeData = new RouteData();
                routeData.Values.Add("controller", controller);
                routeData.Values.Add("action", "index");
                ControllerContext cContext = new ControllerContext { RouteData = routeData, Controller = new FakeController(), HttpContext = context };
                if (model != null)
                    viewData.Model = model;
                var viewResult = ViewEngines.Engines.FindPartialView(cContext, viewName);
                var viewContext = new ViewContext(cContext, viewResult.View, viewData, tempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(cContext, viewResult.View);
                return sw.GetStringBuilder().ToString();

            }
        }

    }

    public class FakeController : Controller
    {
    }
}
