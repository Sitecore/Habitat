namespace Sitecore.Feature.Search.Infrastructure.Pipelines
{
    using System.Web.Routing;
    using Sitecore.Feature.Search;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Pipelines;

    [Service]
    public class InitializeRoutes
    {
        public void Process(PipelineArgs args)
        {
            if (!Context.IsUnitTesting)
            {
                RouteConfig.RegisterRoutes(RouteTable.Routes);
            }
        }
    }
}