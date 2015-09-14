using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Habitat.Search.Models;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Habitat.Search.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult SearchResults(string query)
        {
            return View("SearchResults", GetSearchResults(query));
        }

        public ActionResult SearchBox(string query)
        {
            return View("SearchBox", GetSearchResults(query));
        }

        private SearchResults GetSearchResults(string query)
        {
            var results = this.HttpContext.Items["SearchResults"] as SearchResults;
            if (results != null)
                return results;

            results = new SearchService(RenderingContext.Current.Rendering.Item).Search(query);
            this.HttpContext.Items.Add("SearchResults", results);
            return results;
        }
    }
}
