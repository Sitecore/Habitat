namespace Sitecore.Feature.Accounts.Infrastructure.Pipelines
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Sitecore.Pipelines;

    public class RegisterWebApiRoutes
  {
    public void Process(PipelineArgs args)
    {
      RouteTable.Routes.MapRoute("Feature.Accounts.Api", "api/accounts/{action}", new
                                                                                  {
                                                                                    controller = "Accounts"
                                                                                  });
    }
  }
}