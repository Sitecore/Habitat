namespace Sitecore.Feature.Search.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Sitecore.Diagnostics;
    using Sitecore.Feature.Search.Models;
    using Sitecore.Feature.Search.Repositories;
    using Sitecore.Feature.Search.Services;
    using Sitecore.Foundation.Indexing;
    using Sitecore.Foundation.SitecoreExtensions.Attributes;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Foundation.SitecoreExtensions.Repositories;
    using Sitecore.Mvc.Presentation;

    public class SearchController : Controller
    {
        public SearchController(ISearchContextRepository contextRepository, FacetQueryStringService facetQueryStringService, SearchService searchService, IRenderingPropertiesRepository renderingPropertiesRepository)
        {
            this.SearchContextRepository = contextRepository;
            this.FacetQueryStringService = facetQueryStringService;
            this.SearchService = searchService;
            this.RenderingPropertiesRepository = renderingPropertiesRepository;
        }

        private ISearchContextRepository SearchContextRepository { get; }
        private FacetQueryStringService FacetQueryStringService { get; }
        private SearchService SearchService { get; }
        private IRenderingPropertiesRepository RenderingPropertiesRepository { get; }

        public ActionResult SearchResults(string query)
        {
            var searchResults = this.GetSearchResults(query, null, null);
            return this.View(searchResults.Results);
        }

        public ActionResult PagedSearchResults(string query, int? page, string facets)
        {
            var results = this.GetSearchResults(query, page, facets);
            return this.View(results);
        }

        public ActionResult GlobalSearch()
        {
            var searchContext = this.SearchContextRepository.Get();
            if (searchContext == null)
            {
                Log.Warn("Attempting to show GlobalSearch without a search context", this);
                return new EmptyResult();
            }
            return this.View(this.SearchContextRepository.Get());
        }

        public ActionResult SearchFacets(string query, int? page, string facets)
        {
            var searchResults = this.GetSearchResults(query, page, facets);
            return this.View(searchResults);
        }

        public ActionResult SearchResultsHeader(string query, int? page, string facets)
        {
            var searchContext = this.SearchContextRepository.Get();
            if (searchContext == null)
            {
                Log.Warn("Attempting to show SearchResultsHeader without a search context", this);
                return new EmptyResult();
            }

            var results = new SearchResultsHeader
            {
                Context = this.SearchContextRepository.Get(),
                Results = this.GetSearchResults(query, page, facets)
            };

            return this.View(results);
        }

        [HttpPost]
        [SkipAnalyticsTracking]
        public ActionResult ToggleFacet(string query, string facets, string facetName, string facetValue)
        {
            var resultsUrl = this.SearchContextRepository.Get().SearchResultsUrl;
            var newFacetQueryString = this.FacetQueryStringService.ToggleFacet(facets, facetName, facetValue);
            var url = resultsUrl + $"?query={query}&facets={newFacetQueryString}";
            return new JsonResult {Data = new {query, facets = newFacetQueryString, url}};
        }

        [HttpPost]
        [SkipAnalyticsTracking]
        public ActionResult AjaxSearchResults(string query)
        {
            var searchResults = this.SearchService.SearchFromTopResults(query, 5);
            return this.CreateAjaxResults(searchResults);
        }

        private JsonResult CreateAjaxResults(SearchResultsViewModel searchResults)
        {
            var facet = searchResults.Results.Facets.FirstOrDefault(f => f.Definition.FieldName == Constants.IndexFields.ContentType);
            var results = new
            {
                Results = searchResults.Results.Results.Select(r => new {r.Title, Description = this.TruncateDescription(r.Description), r.ContentType, r.Url, Image = r.Media?.ImageUrl(64, 64)}),
                Facet = new {facet?.Definition.FieldName, facet?.Definition.Title},
                FacetValues = facet?.Values.Select(v => new {v.Title, v.Count, Value = v.Value.ToString()})
            };
            return new JsonResult {Data = new {Results = results, Count = searchResults.Results.TotalNumberOfResults}};
        }

        private string TruncateDescription(string longDescription)
        {
            return longDescription == null ? string.Empty : StringUtil.Clip(StringUtil.RemoveTags(longDescription), 150, true);
        }

        private SearchResultsViewModel GetSearchResults(string query, int? page, string facets)
        {
            if (this.HttpContext.Items.Contains("SearchResults"))
            {
                return this.HttpContext.Items["SearchResults"] as SearchResultsViewModel;
            }

            var pagingSettings = this.RenderingPropertiesRepository.Get<PagingSettings>(RenderingContext.Current.Rendering);
            var viewModel = this.SearchService.Search(query, page, facets, pagingSettings);

            this.HttpContext?.Items.Add("SearchResults", viewModel);
            return viewModel;
        }
    }
}