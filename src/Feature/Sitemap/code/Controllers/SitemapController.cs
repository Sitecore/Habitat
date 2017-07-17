using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.Feature.Sitemap.Repositories;
using Sitecore.Mvc.Presentation;

namespace Sitecore.Feature.Sitemap.Controllers
{
    public class SitemapController : Controller
    {
        private readonly ISitemapRepositories _sitemapRepository;
        public SitemapController() : this(new SitemapRepositories(getRenderingItem()))
        {
        }

        public static Item getRenderingItem()
        {
            if (System.Web.HttpContext.Current.Request.Params["datasource"] != null)
            {
                return null;
            }
            else if (System.Web.HttpContext.Current.Request.QueryString["DS"] == null)
            {
                return RenderingContext.Current.Rendering.Item;
            }
            else
            {
                var queryString = System.Web.HttpContext.Current.Request.QueryString["DS"];
                return Sitecore.Context.Database.GetItem(queryString);
            }
        }

        public SitemapController(ISitemapRepositories sitemapRepository)
        {
            this._sitemapRepository = sitemapRepository;
        }
        public ActionResult Sitemap()
        {
            var items = _sitemapRepository.GetSitemapItems(Sitecore.Context.Item);
            return this.View("Sitemap",items);
        }

        [HttpGet]
        public ActionResult SitemapWithExpend()
        {

            var items = _sitemapRepository.GetSitemapItems(Sitecore.Context.Item);
            return this.View("SitemapWithExpend", items);
        }
        [HttpPost]
        public ActionResult SitemapWithExpend(string itemID)
        {


            var currentItem = Sitecore.Context.Database.GetItem(itemID);
            var items = _sitemapRepository.GetSitemapItemsForAjax(currentItem);
            return this.View("_SitemapWithExpend", items);
        }
    }
}