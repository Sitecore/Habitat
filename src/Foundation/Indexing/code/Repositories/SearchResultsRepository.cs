﻿namespace Sitecore.Foundation.Indexing.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Services;
    using Sitecore.Mvc.Common;

    [Service]
    public class SearchResultsFactory
    {
        public SearchResultsFactory(FormatBooleanFacetService formatBooleanFacetService)
        {
            this.FormatBooleanFacetService = formatBooleanFacetService;
        }

        public ISearchResults Create(SearchResults<SearchResultItem> results, IQuery query)
        {
            var searchResults = this.CreateSearchResults(results);
            var facets = this.CreateFacets(results, query).ToArray();
            return new SearchResults
            {
                Results = searchResults,
                TotalNumberOfResults = results.TotalSearchResults,
                Query = query,
                Facets = facets
            };
        }

        private IEnumerable<ISearchResultFacet> CreateFacets(SearchResults<SearchResultItem> results, IQuery query)
        {
            if (results.Facets == null)
                yield break;

            var facets = CreateFacetsFromProviders();

            foreach (var resultCategory in results.Facets?.Categories)
            {
                IQueryFacet definition;
                if (!facets.TryGetValue(resultCategory.Name.ToLowerInvariant(), out definition))
                    continue;

                var facet = this.CreateFacet(query, resultCategory, definition);
                if (facet == null)
                    continue;

                yield return facet;
            }
        }

        private SearchResultFacet CreateFacet(IQuery query, FacetCategory resultCategory, IQueryFacet definition)
        {
            var facetValues = this.CreateFacetValues(resultCategory, query).ToArray();
            if (!facetValues.Any())
                return null;
            var facet = new SearchResultFacet
            {
                Definition = definition,
                Values = facetValues
            };

            this.FormatFacet(facet);

            return facet;
        }

        private void FormatFacet(SearchResultFacet facet)
        {
            this.FormatBooleanFacetService.Format(facet);

        }

        public FormatBooleanFacetService FormatBooleanFacetService { get; }

        private static Dictionary<string, IQueryFacet> CreateFacetsFromProviders()
        {
            return IndexingProviderRepository.QueryFacetProviders.SelectMany(provider => provider.GetFacets()).Distinct(new GenericEqualityComparer<IQueryFacet>((facet, queryFacet) => facet.FieldName == queryFacet.FieldName, facet => facet.FieldName.GetHashCode())).ToDictionary(facet => facet.FieldName, facet => facet);
        }

        private IEnumerable<ISearchResultFacetValue> CreateFacetValues(FacetCategory resultCategory, IQuery query)
        {
            foreach (var resultValue in resultCategory.Values)
            {
                var facetValue = new SearchResultFacetValue
                {
                    Value = resultValue.Name,
                    Count = resultValue.AggregateCount,
                    Selected = this.IsFacetValueSelected(resultCategory, query, resultValue),
                    Title = resultValue.Name
                };
                yield return facetValue;
            }
        }

        private bool IsFacetValueSelected(FacetCategory resultCategory, IQuery query, FacetValue resultValue)
        {
            if (query.Facets == null)
                return false;
            return query.Facets.Any(f => f.Key.Equals(resultCategory.Name, StringComparison.InvariantCultureIgnoreCase) && f.Value.Any(v => v.Equals(resultValue.Name, StringComparison.InvariantCultureIgnoreCase)));
        }

        private IEnumerable<ISearchResult> CreateSearchResults(SearchResults<SearchResultItem> results)
        {
            return results.Hits.Select(h => SearchResultFactory.Create(h.Document)).ToArray();
        }
    }
}