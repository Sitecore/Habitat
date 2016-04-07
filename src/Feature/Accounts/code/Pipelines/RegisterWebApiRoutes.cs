namespace Sitecore.Feature.Accounts.Pipelines
{
  using System.Web.Mvc;
  using System.Web.Routing;
  using Sitecore.Pipelines;
  using Sitecore.Shell.Framework.Commands.Masters;

  public class RegisterWebApiRoutes
  {
    public void Process(PipelineArgs args)
    {
      RouteTable.Routes.MapRoute(
        name: "Feature.Accounts.Api",
        url: "api/accounts/{action}",
        defaults:new {controller = "Accounts"});
    }
  }
}