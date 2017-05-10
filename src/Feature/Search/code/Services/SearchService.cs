using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Search.Services
{
    using Sitecore.Analytics;
    using Sitecore.Analytics.Data;
    using Sitecore.Feature.Search.Factories;
    using Sitecore.Feature.Search.Models;
    using Sitecore.Feature.Search.Repositories;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Services;
    using Sitecore.Mvc.Presentation;

    [Service]
    public class SearchService
    {
        public SearchService(SearchResultsViewModelFactory searchResultsViewModelFactory, ISearchServiceRepository searchServiceRepository, ISearchContextRepository searchContextRepository, FacetQueryStringService facetQueryStringService, ITrackerService trackerService)
        {
            this.SearchResultsViewModelFactory = searchResultsViewModelFactory;
            this.SearchServiceRepository = searchServiceRepository;
            this.SearchContextRepository = searchContextRepository;
            this.FacetQueryStringService = facetQueryStringService;
            this.TrackerService = trackerService;
        }

        private ISearchServiceRepository SearchServiceRepository { get; }
        private SearchResultsViewModelFactory SearchResultsViewModelFactory { get; }
        private ISearchContextRepository SearchContextRepository { get; }
        private FacetQueryStringService FacetQueryStringService { get; }
        private ITrackerService TrackerService { get; set; }

        public SearchResultsViewModel Search(string query, int? page, string facets, PagingSettings pagingSettings)
        {
            var pageNumber = page == null ? 0 : page < 0 ? 0 : page.Value;
            query = query ?? string.Empty;

            var searchQuery = new SearchQuery
            {
                QueryText = query,
                Page = pageNumber,
                NoOfResults = pagingSettings.ResultsOnPage,
                Facets = this.FacetQueryStringService.ParseFacets(facets)
            };

            var results = this.SearchServiceRepository.Get(this.SearchContextRepository.Get()).Search(searchQuery);
            this.RegisterSearchPageEvent(query, results);
            return this.SearchResultsViewModelFactory.Create(searchQuery, results, pagingSettings.PagesToShow, pagingSettings.ResultsOnPage);
        }

        private void RegisterSearchPageEvent(string searchQuery, ISearchResults results)
        {
            this.TrackerService.TrackPageEvent(AnalyticsIds.SearchEvent, searchQuery, searchQuery, searchQuery?.ToLowerInvariant(), results.TotalNumberOfResults);
            if (results.TotalNumberOfResults == 0)
                this.TrackerService.TrackPageEvent(AnalyticsIds.NoSearchHitsFound, searchQuery, searchQuery, searchQuery?.ToLowerInvariant());
        }

        public SearchResultsViewModel SearchFromTopResults(string query, int count)
        {
            var searchQuery = new SearchQuery
            {
                QueryText = query,
                Page = 0,
                NoOfResults = count
            };

            var results = this.SearchServiceRepository.Get(this.SearchContextRepository.Get()).Search(searchQuery);
            return this.SearchResultsViewModelFactory.Create(searchQuery, results, 1, count);
        }
    }
}