namespace Sitecore.Feature.Search.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Sitecore.Diagnostics;
    using Sitecore.Feature.Search.Models;
    using Sitecore.Feature.Search.Repositories;
    using Sitecore.Feature.Search.Services;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Repositories;

    public class SearchController : Controller
    {
        public SearchController(FacetQueryStringService facetQueryStringService) : this(new SearchContextRepository(), new RenderingPropertiesRepository(), facetQueryStringService)
        {
        }

        public SearchController(ISearchContextRepository contextRepository, IRenderingPropertiesRepository renderingPropertiesRepository, FacetQueryStringService facetQueryStringService) : this(new SearchServiceRepository(contextRepository.Get()), contextRepository, renderingPropertiesRepository, facetQueryStringService)
        {
        }

        public SearchController(ISearchServiceRepository serviceRepository, ISearchContextRepository contextRepository, IRenderingPropertiesRepository renderingPropertiesRepository, FacetQueryStringService facetQueryStringService)
        {
            this.SearchServiceRepository = serviceRepository;
            this.SearchContextRepository = contextRepository;
            this.RenderingPropertiesRepository = renderingPropertiesRepository;
            this.FacetQueryStringService = facetQueryStringService;
        }

        private ISearchServiceRepository SearchServiceRepository { get; }
        private ISearchContextRepository SearchContextRepository { get; }
        private IRenderingPropertiesRepository RenderingPropertiesRepository { get; }
        private FacetQueryStringService FacetQueryStringService { get; }

        public ActionResult SearchResults(string query)
        {
            var searchResults = this.GetSearchResults(query, null, null);
            return this.View(searchResults.Results);
        }

        public ActionResult GlobalSearch()
        {
            var searchContext = this.GetSearchContext();
            if (searchContext == null)
            {
                Log.Warn("Attempting to show GlobalSearch without a search context", this);
                return new EmptyResult();
            }
            return this.View(searchContext);
        }

        public ActionResult SearchFacets(string query, int? page, string facets)
        {
            var searchResults = this.GetSearchResults(query, null, null);
            return this.View(searchResults.Results);
        }

        public ActionResult SearchResultsHeader(string query, int? page, string facets)
        {
            var results = this.SearchResultsHeaderModel(query, page, facets);
            return this.View(results);
        }

        private SearchResultsHeader SearchResultsHeaderModel(string query, int? page, string facets)
        {
            return new SearchResultsHeader()
            {
                Context = this.GetSearchContext(),
                Results = this.GetSearchResults(query, page, facets)
            };
        }

        public ActionResult PagedSearchResults(string query, int? page, string facets)
        {
            var results = this.GetSearchResults(query, page, facets);
            return this.View(results);
        }

        private PagedSearchResults GetSearchResults(string query, int? page, string facets)
        {
            var pagingSettings = this.RenderingPropertiesRepository.Get<PagingSettings>();
            var resultsOnPage = pagingSettings.ResultsOnPage <= 1 ? Models.PagedSearchResults.DefaultResultsOnPage : pagingSettings.ResultsOnPage;
            var searchQuery = new SearchQuery
            {
                QueryText = query,
                IndexOfFirstResult = GetIndexOfFirstResult(page, resultsOnPage),
                NoOfResults = resultsOnPage,
                Facets = this.FacetQueryStringService.ParseFacets(facets)
            };
            var results = this.GetSearchResults(searchQuery);
            var pageable = new PagedSearchResults(searchQuery, results.TotalNumberOfResults, pagingSettings.PagesToShow, resultsOnPage)
            {
                Query = query,
                Results = results
            };
            return pageable;
        }

        private static int GetIndexOfFirstResult(int? page, int resultsOnPage)
        {
            var pageValue = page ?? 1;
            return pageValue < 1 ? 0 : pageValue * resultsOnPage;
        }

        private ISearchResults GetSearchResults(SearchQuery searchQuery)
        {
            ISearchResults results = null;
            if (this.HttpContext != null)
            {
                results = this.HttpContext.Items["SearchResults"] as ISearchResults;
            }

            if (results != null)
            {
                return results;
            }

            results = this.SearchServiceRepository.Get().Search(searchQuery);
            this.HttpContext?.Items.Add("SearchResults", results);
            return results;
        }

        private SearchContext GetSearchContext()
        {
            return this.SearchContextRepository.Get();
        }
    }
}