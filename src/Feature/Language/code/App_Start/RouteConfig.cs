namespace Sitecore.Feature.Language
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("language-changelanguage",
                            "api/feature/language/changelanguage", 
                            new { controller = "Language", action = "ChangeLanguage", id = UrlParameter.Optional });
        }
    }
}