using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habitat.Accounts.Pipelines
{
  using System.Web.Http;
  using Sitecore.Pipelines;

  public class RegisterWebApiRoutes
  {
    public void Process(PipelineArgs args)
    {
      GlobalConfiguration.Configure(WebApiConfig.Register);
    }
  }
}