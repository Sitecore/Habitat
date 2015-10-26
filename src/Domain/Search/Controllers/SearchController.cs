using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Habitat.Framework.Indexing;
using Habitat.Framework.Indexing.Models;
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

        public ActionResult GlobalSearch()
        {
            return View("GlobalSearch", GetSearchSettings());
        }

        public ActionResult SearchSettings(string query)
        {
            return View("SearchSettings", GetSearchSettings(query));
        }

        private ISearchResults GetSearchResults(string queryText)
        {
            var results = this.HttpContext.Items["SearchResults"] as ISearchResults;
            if (results != null)
                return results;

            var query = CreateQuery(queryText);
            results = SearchServiceRepository.Get().Search(query);
            this.HttpContext.Items.Add("SearchResults", results);
            return results;
        }

        private IQuery CreateQuery(string queryText)
        {
            return QueryRepository.Get(queryText);
        }

        private SearchSettings GetSearchSettings(string query = null)
        {
            return SearchSettingsRepository.Get(query);
        }
    }
}
