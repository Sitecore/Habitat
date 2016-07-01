namespace Sitecore.Feature.News.Controllers
{
  using System.Web.Mvc;
  using Sitecore.Feature.News.Repositories;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Mvc.Presentation;

  public class NewsController : Controller
  {
    private readonly INewsRepositoryFactory newsRepositoryFactory;

    public NewsController() : this(new NewsRepositoryFactory())
    {
    }

    public NewsController(INewsRepositoryFactory newsRepositoryFactory)
    {
      this.newsRepositoryFactory = newsRepositoryFactory;
    }

    public ActionResult NewsList()
    {
      var items = this.newsRepositoryFactory.Create(RenderingContext.Current.Rendering.Item).Get();
      return this.View("NewsList", items);
    }

    public ActionResult LatestNews()
    {
      //TODO: change to parameter template
      var count = RenderingContext.Current.Rendering.GetIntegerParameter("count", 5);
      var items = this.newsRepositoryFactory.Create(RenderingContext.Current.Rendering.Item).GetLatestNews(count);
      return this.View("LatestNews", items);
    }
  }
}