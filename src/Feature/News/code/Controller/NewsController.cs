namespace Sitecore.Feature.News.Controller
{
  using System.Web.Mvc;
  using Sitecore.Feature.News.Repositories;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Mvc.Presentation;

  public class NewsController : Controller
  {
    private readonly INewsRepositoryCreator _newsRepositoryCreator;

    public NewsController() : this(new NewsRepositoryCreator())
    {
    }

    public NewsController(INewsRepositoryCreator newsRepositoryCreator)
    {
      _newsRepositoryCreator = newsRepositoryCreator;
    }

    public ActionResult NewsList()
    {
      var items = this._newsRepositoryCreator.Create(RenderingContext.Current.Rendering.Item).Get();
      return this.View("NewsList", items);
    }

    public ActionResult LatestNews()
    {
      var count = RenderingContext.Current.Rendering.GetIntegerParameter("count", 5);
      var items = this._newsRepositoryCreator.Create(RenderingContext.Current.Rendering.Item).GetLatestNews(count);
      return this.View("LatestNews", items);
    }
  }
}