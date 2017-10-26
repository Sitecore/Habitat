namespace Sitecore.Feature.Demo.Pipelines
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Sitecore.Pipelines;

    public class RegisterWebApiRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("Feature.Demo.Api", "api/demo/{action}", new
            {
                controller = "Demo"
            });
        }
    }
}