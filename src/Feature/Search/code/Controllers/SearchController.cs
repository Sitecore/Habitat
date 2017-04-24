namespace Sitecore.Feature.Search.Controllers
{
    using System.Web.Mvc;
    using Sitecore.Diagnostics;
    using Sitecore.Feature.Search.Factories;
    using Sitecore.Feature.Search.Models;
    using Sitecore.Feature.Search.Repositories;
    using Sitecore.Feature.Search.Services;
    using Sitecore.Foundation.Indexing.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Attributes;
    using Sitecore.Foundation.SitecoreExtensions.Repositories;
    using Sitecore.Mvc.Presentation;

    public class SearchController : Controller
    {
        public SearchController(ISearchServiceRepository serviceRepository, ISearchContextRepository contextRepository, IRenderingPropertiesRepository renderingPropertiesRepository, FacetQueryStringService facetQueryStringService, SearchResultsViewModelFactory searchResultsViewModelFactory)
        {
            this.SearchServiceRepository = serviceRepository;
            this.SearchContextRepository = contextRepository;
            this.RenderingPropertiesRepository = renderingPropertiesRepository;
            this.FacetQueryStringService = facetQueryStringService;
            this.SearchResultsViewModelFactory = searchResultsViewModelFactory;
        }

        private ISearchServiceRepository SearchServiceRepository { get; }
        private ISearchContextRepository SearchContextRepository { get; }
        private IRenderingPropertiesRepository RenderingPropertiesRepository { get; }
        private FacetQueryStringService FacetQueryStringService { get; }
        private SearchResultsViewModelFactory SearchResultsViewModelFactory { get; }

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
            var newFacetQueryString = this.FacetQueryStringService.ToggleFacet(facets, facetName, facetValue);
            var url = $"?query={query}&facets={newFacetQueryString}";
            return new JsonResult { Data = new {query, facets = newFacetQueryString, url} };
        }


        private SearchResultsViewModel GetSearchResults(string query, int? page, string facets)
        {
            if (this.HttpContext.Items.Contains("SearchResults"))
            {
                return this.HttpContext.Items["SearchResults"] as SearchResultsViewModel;
            }

            var pagingSettings = this.RenderingPropertiesRepository.Get<PagingSettings>(RenderingContext.Current.Rendering);
            var pageNumber = page == null ? 0 : page < 0 ? 0 : page.Value;

            var searchQuery = new SearchQuery
            {
                QueryText = query,
                Page = pageNumber,
                NoOfResults = pagingSettings.ResultsOnPage,
                Facets = this.FacetQueryStringService.ParseFacets(facets)
            };

            var results = this.SearchServiceRepository.Get(this.SearchContextRepository.Get()).Search(searchQuery);

            var viewModel = this.SearchResultsViewModelFactory.Create(searchQuery, results, pagingSettings.PagesToShow, pagingSettings.ResultsOnPage);

            this.HttpContext?.Items.Add("SearchResults", viewModel);
            return viewModel;
        }
    }
}