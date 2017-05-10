namespace Sitecore.Foundation.Indexing.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.Linq;
    using Sitecore.ContentSearch.Linq.Utilities;
    using Sitecore.ContentSearch.SearchTypes;
    using Sitecore.ContentSearch.Utilities;
    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Repositories;
    using Sitecore.Mvc.Common;

    public class SearchService
    {
        public SearchService(ISearchSettings settings)
        {
            this.Settings = settings;
            this.SearchIndexResolver = DependencyResolver.Current.GetService<SearchIndexResolver>();
            this.SearchResultsFactory = DependencyResolver.Current.GetService<SearchResultsFactory>();
        }

        public virtual ISearchSettings Settings { get; set; }

        public SitecoreIndexableItem ContextItem
        {
            get
            {
                var contextItem = this.Settings.Root ?? Context.Item;
                Assert.IsNotNull(contextItem, "Could not determine a context item for the search");
                return contextItem;
            }
        }

        private SearchIndexResolver SearchIndexResolver { get; }

        private SearchResultsFactory SearchResultsFactory { get; }

        public IEnumerable<IQueryRoot> QueryRoots => IndexingProviderRepository.QueryRootProviders.Union(new[] {this.Settings});


        public virtual ISearchResults Search(IQuery query)
        {
            using (var context = this.SearchIndexResolver.GetIndex(this.ContextItem).CreateSearchContext())
            {
                var queryable = this.CreateAndInitializeQuery(context);

                queryable = this.AddContentPredicates(queryable, query);
                queryable = this.AddFacets(queryable, query);
                queryable = this.AddPaging(queryable, query);
                var results = queryable.GetResults();
                return this.SearchResultsFactory.Create(results, query);
            }
        }

        private IQueryable<SearchResultItem> AddPaging(IQueryable<SearchResultItem> queryable, IQuery query)
        {
            return queryable.Page(query.Page < 0 ? 0 : query.Page, query.NoOfResults <= 0 ? 10 : query.NoOfResults);
        }

        public virtual ISearchResults FindAll()
        {
            return this.FindAll(0, 0);
        }

        public virtual ISearchResults FindAll(int skip, int take)
        {
            using (var context = ContentSearchManager.GetIndex(this.ContextItem).CreateSearchContext())
            {
                var queryable = this.CreateAndInitializeQuery(context);

                if (skip > 0)
                {
                    queryable = queryable.Skip(skip);
                }
                if (take > 0)
                {
                    queryable = queryable.Take(take);
                }

                var results = queryable.GetResults();
                return this.SearchResultsFactory.Create(results, null);
            }
        }

        private IQueryable<SearchResultItem> CreateAndInitializeQuery(IProviderSearchContext context)
        {
            var queryable = context.GetQueryable<SearchResultItem>();
            queryable = this.InitializeQuery(queryable);
            return queryable;
        }

        private IQueryable<SearchResultItem> InitializeQuery(IQueryable<SearchResultItem> queryable)
        {
            queryable = this.SetQueryRoots(queryable);
            queryable = this.FilterOnLanguage(queryable);
            queryable = this.FilterOnVersion(queryable);
            if (this.Settings.MustHaveFormatter)
            {
                queryable = this.FilterOnHasSearchResultFormatter(queryable);
            }
            if (this.Settings.Templates != null && this.Settings.Templates.Any())
            {
                queryable = queryable.Cast<IndexedItem>().Where(this.GetTemplatePredicates(this.Settings.Templates));
            }
            else
            {
                queryable = this.FilterOnItemsMarkedAsIndexable(queryable);
            }
            return queryable;
        }

        private IQueryable<SearchResultItem> FilterOnHasSearchResultFormatter(IQueryable queryable)
        {
            return queryable.Cast<IndexedItem>().Where(i => i.HasSearchResultFormatter);
        }

        private IQueryable<SearchResultItem> FilterOnItemsMarkedAsIndexable(IQueryable<SearchResultItem> queryable)
        {
            var indexedItemPredicate = this.GetPredicateForItemDerivesFromIndexedItem();
            var contentTemplatePredicates = this.GetPredicatesForContentTemplates();
            return queryable.Cast<IndexedItem>().Where(indexedItemPredicate.And(contentTemplatePredicates));
        }

        private Expression<Func<IndexedItem, bool>> GetPredicatesForContentTemplates()
        {
            var contentTemplatePredicates = PredicateBuilder.False<IndexedItem>();
            foreach (var provider in IndexingProviderRepository.QueryPredicateProviders)
            {
                contentTemplatePredicates = contentTemplatePredicates.Or(this.GetTemplatePredicates(provider.SupportedTemplates));
            }
            return contentTemplatePredicates;
        }

        private Expression<Func<IndexedItem, bool>> GetPredicateForItemDerivesFromIndexedItem()
        {
            var notIndexedItem = PredicateBuilder.Create<IndexedItem>(i => !i.AllTemplates.Contains(IdHelper.NormalizeGuid(Templates.IndexedItem.ID)));
            var indexedItemWithShowInResults = PredicateBuilder.And<IndexedItem>(i => i.AllTemplates.Contains(IdHelper.NormalizeGuid(Templates.IndexedItem.ID)), i => i.ShowInSearchResults);

            return notIndexedItem.Or(indexedItemWithShowInResults);
        }

        private Expression<Func<IndexedItem, bool>> GetTemplatePredicates(IEnumerable<ID> templates)
        {
            var expression = PredicateBuilder.False<IndexedItem>();
            foreach (var template in templates)
            {
                expression = expression.Or(i => i.AllTemplates.Contains(IdHelper.NormalizeGuid(template)));
            }
            return expression;
        }

        private IQueryable<SearchResultItem> AddFacets(IQueryable<SearchResultItem> queryable, IQuery query)
        {
            var facets = GetFacetsFromProviders();

            var addedFacetPredicate = false;
            var facetPredicate = PredicateBuilder.True<SearchResultItem>();
            foreach (var facet in facets)
            {
                if (string.IsNullOrEmpty(facet.FieldName))
                    continue;

                if (query.Facets != null && query.Facets.ContainsKey(facet.FieldName))
                {
                    var facetValues = query.Facets[facet.FieldName];

                    var facetValuePredicate = PredicateBuilder.False<SearchResultItem>();
                    foreach (var facetValue in facetValues)
                    {
                        if (facetValue == null)
                            continue;
                        facetValuePredicate = facetValuePredicate.Or(item => item[facet.FieldName] == facetValue);
                    }
                    facetPredicate = facetPredicate.And(facetValuePredicate);
                    addedFacetPredicate = true;
                }
                queryable = queryable.FacetOn(item => item[facet.FieldName]);
            }
            if (addedFacetPredicate)
                queryable = queryable.Where(facetPredicate);

            return queryable;
        }

        private static IEnumerable<IQueryFacet> GetFacetsFromProviders()
        {
            return IndexingProviderRepository.QueryFacetProviders.SelectMany(provider => provider.GetFacets()).Distinct(new GenericEqualityComparer<IQueryFacet>((facet, queryFacet) => facet.FieldName == queryFacet.FieldName, facet => facet.FieldName.GetHashCode()));
        }

        private IQueryable<SearchResultItem> FilterOnLanguage(IQueryable<SearchResultItem> queryable)
        {
            queryable = queryable.Filter(item => item.Language == Context.Language.Name);
            return queryable;
        }

        private IQueryable<SearchResultItem> FilterOnVersion(IQueryable<SearchResultItem> queryable)
        {
            queryable = queryable.Cast<IndexedItem>().Filter(item => item.IsLatestVersion);
            return queryable;
        }

        private IQueryable<SearchResultItem> SetQueryRoots(IQueryable<SearchResultItem> queryable)
        {
            var rootPredicates = PredicateBuilder.False<SearchResultItem>();

            foreach (var provider in this.QueryRoots)
            {
                if (provider.Root == null)
                {
                    continue;
                }
                rootPredicates = rootPredicates.Or(item => item.Path.StartsWith(provider.Root.Paths.FullPath));
            }

            return queryable.Where(rootPredicates);
        }

        private IQueryable<SearchResultItem> AddContentPredicates(IQueryable<SearchResultItem> queryable, IQuery query)
        {
            var contentPredicates = PredicateBuilder.False<SearchResultItem>();
            foreach (var provider in IndexingProviderRepository.QueryPredicateProviders)
            {
                contentPredicates = contentPredicates.Or(provider.GetQueryPredicate(query));
            }
            return queryable.Where(contentPredicates);
        }
    }
}