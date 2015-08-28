using System.Web.Mvc;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Habitat.News.Repositories;
using Sitecore.Mvc.Presentation;

namespace Habitat.News.Controller
{
    public class NewsController : System.Web.Mvc.Controller
    {
        private readonly INewsRepository _newsRepository;

        public NewsController() : this(new NewsRepository(RenderingContext.Current.Rendering.Item))
        {
        }

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public ActionResult NewsList()
        {
            var items = _newsRepository.Get();
            return View("NewsList", items);
        }

        public ActionResult LatestNews()
        {
            var count = RenderingContext.Current.Rendering.GetIntegerParameter("count", 5);
            var items = _newsRepository.GetLatestNews(count);
            return View("LatestNews", items);
        }
    }
}