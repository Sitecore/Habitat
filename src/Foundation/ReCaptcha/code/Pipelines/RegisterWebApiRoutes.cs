namespace Sitecore.Foundation.ReCaptcha.Pipelines
{
    using System.Web.Http;
    using System.Web.Routing;
    using Sitecore.Pipelines;

    public class RegisterWebApiRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapHttpRoute("Foundation.ReCaptcha.WebApi", "WebAPI/Foundation.ReCaptcha/{action}", new { controller = "ReCaptcha" });
        }
    }
}