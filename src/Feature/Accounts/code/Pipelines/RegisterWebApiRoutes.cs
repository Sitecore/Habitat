﻿namespace Sitecore.Feature.Accounts.Pipelines
{
  using System.Web.Mvc;
  using System.Web.Routing;
  using Sitecore.Pipelines;

  public class RegisterWebApiRoutes
  {
    public void Process(PipelineArgs args)
    {
      RouteTable.Routes.MapRoute(
        name: "Api",
        url: "api/{controller}/{action}");
      //GlobalConfiguration.Configure(WebApiConfig.Register);
    }
  }
}