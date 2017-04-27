using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            return new SearchResultsViewModel
            {
                VisiblePagesCount = pagesToShow,
                TotalResults = results.TotalNumberOfResults,
                ResultsOnPage = resultsOnPage,
                Query = searchQuery.QueryText,
                Facets = this.FacetQueryStringService.GetFacetQueryString(searchQuery.Facets),
                Results = results,
                Page = searchQuery.Page
            };
        }

        private FacetQueryStringService FacetQueryStringService { get; }
    }
}