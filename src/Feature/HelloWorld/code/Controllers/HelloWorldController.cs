namespace Sitecore.Feature.HelloWorld.Controllers
{
    using System.Web.Mvc;
    using Glass.Mapper.Sc;
    using Sitecore.Feature.HelloWorld.Models;
    using Sitecore.Mvc.Presentation;

    public class HelloWorldController : Controller
    {
        #region Methods

        public ActionResult Hello()
        {
            var glassContext = new SitecoreContext();
            var viewModel = new HelloViewModel
            {
                Text = "World",
                Hello = glassContext.GetItem<I_HelloWorld>(RenderingContext.Current.Rendering.DataSource)
            };
            return View("Hello", viewModel);
        }

        #endregion
    }
}