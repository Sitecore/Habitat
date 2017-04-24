namespace Sitecore.Feature.Search
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("search-togglefacet", "api/feature/search/togglefacet", new { controller = "Search", action = "ToggleFacet", id = UrlParameter.Optional });
        }
   }
}