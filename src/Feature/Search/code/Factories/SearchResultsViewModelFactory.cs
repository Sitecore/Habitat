namespace Sitecore.Feature.Search.Factories
{
    using Sitecore.Feature.Search.Models;
    using Sitecore.Feature.Search.Services;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Indexing.Models;

    [Service]
    public class SearchResultsViewModelFactory
    {
        public SearchResultsViewModelFactory(FacetQueryStringService facetQueryStringService)
        {
            this.FacetQueryStringService = facetQueryStringService;
        }

        public SearchResultsViewModel Create(IQuery searchQuery, ISearchResults results, int pagesToShow, int resultsOnPage)
        {
            var facets = searchQuery.Facets == null ? null : this.FacetQueryStringService.GetFacetQueryString(searchQuery.Facets);
            return new SearchResultsViewModel
            {
                VisiblePagesCount = pagesToShow,
                TotalResults = results.TotalNumberOfResults,
                ResultsOnPage = resultsOnPage,
                Query = searchQuery.QueryText,
                Facets = facets,
                Results = results,
                Page = searchQuery.Page
            };
        }

        private FacetQueryStringService FacetQueryStringService { get; }
    }
}