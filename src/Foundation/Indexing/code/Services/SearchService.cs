namespace Sitecore.Foundation.Indexing.Services
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.Linq;
  using Sitecore.ContentSearch.Linq.Utilities;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.Indexing.Infrastructure;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.Indexing.Repositories;

  public class SearchService
  {
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

    public SearchService(ISearchSettings settings)
    {
      this.Settings = settings;
    }


    public virtual ISearchResults Search(IQuery query)
    {
      using (var context = ContentSearchManager.GetIndex(this.ContextItem).CreateSearchContext())
      {
        var queryable = this.CreateAndInitializeQuery(context);

        if (this.Settings.Templates == null || !this.Settings.Templates.Any())
          queryable = this.FilterOnItemsMarkedAsIndexable(queryable);
        queryable = this.AddContentPredicates(queryable, query);
        queryable = this.AddFacets(queryable);
        if (query.IndexOfFirstResult > 0)
        {
          queryable = queryable.Skip(query.IndexOfFirstResult);
        }
        if (query.NoOfResults > 0)
        {
          queryable = queryable.Take(query.NoOfResults);
        }
        var results = queryable.GetResults();
        return SearchResultsFactory.Create(results, query);
      }
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
          queryable = queryable.Skip(skip);
        if (take > 0)
          queryable = queryable.Take(take);

        var results = queryable.GetResults();
        return SearchResultsFactory.Create(results, null);
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
      queryable = this.SetQueryRoot(queryable);
      queryable = this.FilterOnLanguage(queryable);
      queryable = this.FilterOnVersion(queryable);
      queryable = this.FilterOnHasSearchResultFormatter(queryable);
      if (this.Settings.Templates != null && this.Settings.Templates.Any())
        queryable = queryable.Cast<IndexedItem>().Where(this.GetTemplatePredicates(this.Settings.Templates));
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
      var templatePredicate = PredicateBuilder.True<IndexedItem>();
      templatePredicate = templatePredicate.And(i => i.AllTemplates.Contains(IdHelper.NormalizeGuid(Templates.IndexedItem.ID)));
      return templatePredicate;
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

    private IQueryable<SearchResultItem> FilterOnPresentationOnly(IQueryable queryable)
    {
      return queryable.Cast<IndexedItem>().Where(i => i.HasPresentation && i.ShowInSearchResults);
    }

    private IQueryable<SearchResultItem> AddFacets(IQueryable<SearchResultItem> queryable)
    {
      queryable = queryable.FacetOn(item => item.TemplateName);
      return queryable;
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

    private IQueryable<SearchResultItem> SetQueryRoot(IQueryable<SearchResultItem> queryable)
    {
      var root = this.Settings.Root;
      if (root == null)
        throw new InvalidOperationException("Search root must be set");

      queryable = queryable.Where(item => item.Path.StartsWith(root.Paths.FullPath));
      return queryable;
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