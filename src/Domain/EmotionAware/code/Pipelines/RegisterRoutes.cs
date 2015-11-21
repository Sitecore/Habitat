namespace Habitat.EmotionAware.Pipelines
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Sitecore.Pipelines;

    public class RegisterRoutes
    {
        public void Process(PipelineArgs args)
        {

            RouteTable.Routes.MapRoute(
               "HabitatEmotions",
               "data/emotions/{imageStream}",
               new { controller = " EmotionAware", action = "RegisterEmotion" }
           );


        }
    }
}