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
            routes.MapRoute("search-ajaxsearch", "api/feature/search/ajaxsearch", new { controller = "Search", action = "AjaxSearchResults", id = UrlParameter.Optional });
        }
    }
}