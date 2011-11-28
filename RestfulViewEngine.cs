using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace Chinchillet
{
    public class RestfulViewEngine : VirtualPathProviderViewEngine
    {
        public RestfulViewEngine()
        {
            MasterLocationFormats = new[] { "~/Shared/{0}_html.cshtml" };
            ViewLocationFormats = new[] { "~/Views/{1}/{0}_html.cshtml" };
            PartialViewLocationFormats = new[] { "~/Views/{1}/{0}_html.cshtml", "~/Common/{0}_html.cshtml" };
        }
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new RazorView(controllerContext, partialPath, null, true, new string[] { ".cshtml" });
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var format = Path.GetExtension(controllerContext.RequestContext.HttpContext.Request.RawUrl).Replace(".", "");
            format = (string.IsNullOrEmpty(format) ? "html" : format);
            controllerContext.RouteData.DataTokens["format"] = format;
            viewPath = viewPath.Replace("_html.cshtml", String.Format("_{0}.cshtml", format));
            return new RazorView(controllerContext, viewPath, masterPath, true, null);
        }
    }
}