namespace Sitecore.Feature.News.Controllers
{
	using System.Web.Mvc;
	using Sitecore.Feature.News.Models.Feature.News;
	using Sitecore.Feature.News.Repositories;
	using Sitecore.Foundation.SitecoreExtensions.Extensions;
	using Sitecore.Mvc.Presentation;
	using Synthesis.Mvc.UI;

	public class NewsController : Controller
	{
		//private readonly INewsRepositoryFactory newsRepositoryFactory;

		//public NewsController() : this(new NewsRepositoryFactory())
		//{
		//}

		//public NewsController(INewsRepositoryFactory newsRepositoryFactory)
		//{
		//	this.newsRepositoryFactory = newsRepositoryFactory;
		//}

		//public ActionResult NewsList()
		//{
		//	var items = this.newsRepositoryFactory.Create(RenderingContext.Current.Rendering.Item).Get();
		//	return this.View("NewsList", items);
		//}

		//public ActionResult LatestNews()
		//{
		//	var count = RenderingContext.Current.Rendering.GetIntegerParameter("count", 5);
		//	var items = this.newsRepositoryFactory.Create(RenderingContext.Current.Rendering.Item).GetLatestNews(count);
		//	return this.View("LatestNews", items);
		//}

		private readonly ISynthesisNewsRepositoryFactory newsRepositoryFactory;
		private readonly IRenderingContext renderingContext;

		public NewsController() : this(new SynthesisNewsRepositoryFactory(), new SitecoreRenderingContext())
		{
		}

		public NewsController(ISynthesisNewsRepositoryFactory newsRepositoryFactory, IRenderingContext renderingContext)
		{
			this.newsRepositoryFactory = newsRepositoryFactory;
			this.renderingContext = renderingContext;
		}

		public ActionResult NewsList()
		{
			var dataSource = this.renderingContext.GetRenderingDatasource<I_NewsFolderItem>();

			if(dataSource == null) return this.Content("Derp.");

			var items = this.newsRepositoryFactory.Create(dataSource).Get(500);

			return this.View("SynthesisNewsList", items);
		}

		public ActionResult LatestNews()
		{
			var dataSource = this.renderingContext.GetRenderingDatasource<I_NewsFolderItem>();

			if (dataSource == null) return this.Content("Derp.");

			var count = this.renderingContext.Parameters.GetIntegerParameter("count", 5);

			var items = this.newsRepositoryFactory.Create(dataSource).Get(count);

			return this.View("SynthesisLatestNews", items);
		}
	}
}