using System.Web.Mvc;
using System.Web.Routing;

namespace Cibertec.Angular
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {Controller="Default", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NoMatch",
                url: "*",
                defaults: new { controller = "Default", action = "Index" }
            );
        }
    }
}

