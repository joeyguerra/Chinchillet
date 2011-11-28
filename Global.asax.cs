using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace Chinchillet
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Format", "{controller}.{format}", new { controller = "Index", action = "Index", format = "html" });
            routes.MapRoute("FormatWithId", "{controller}/{id}.{format}", new { controller = "Index", action = "Index", id = UrlParameter.Optional, format = "html" });
            routes.MapRoute("WithoutId", "{controller}", new { controller = "Index", action = "Index", format = "html" });
            routes.MapRoute("Default", "{controller}/{id}", new { controller = "Index", action = "Index", id = UrlParameter.Optional, format = "html" });
        }
        public static void RegisterViewEngines(ViewEngineCollection engines)
        {
            engines.Clear();
            engines.Add(new RestfulViewEngine());
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            RegisterViewEngines(ViewEngines.Engines);
        }
    }
}