using System.Web.Mvc;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore.Mvc.Presentation;

namespace Habitat.News.Controller
{
    public class NewsController : System.Web.Mvc.Controller
    {
        private readonly INewsService _newsService;

        public NewsController() : this(new NewsService(RenderingContext.Current.Rendering.Item))
        {
        }

        public NewsController(INewsService newsService)
        {
            this._newsService = newsService;
        }

        public ActionResult NewsList()
        {
            var items = this._newsService.GetNews();
            return this.View("NewsList", items);
        }

        public ActionResult LatestNews()
        {
            var count = RenderingContext.Current.Rendering.GetIntegerParameter("count", 5);
            var items = this._newsService.GetLatestNews(count);
            return this.View("LatestNews", items);
        }
    }
}