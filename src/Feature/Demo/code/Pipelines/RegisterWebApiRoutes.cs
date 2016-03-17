namespace Sitecore.Feature.Demo.Pipelines
{
  using System.Web.Mvc;
  using System.Web.Routing;
  using Sitecore.Pipelines;

  public class RegisterWebApiRoutes
  {
    public void Process(PipelineArgs args)
    {
      RouteTable.Routes.MapRoute(
        name: "Feature.Demo.Api",
        url: "api/demo/{action}",
        defaults: new { controller = "Demo" });
    }
  }
}