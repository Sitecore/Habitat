namespace Habitat.Framework.Indexing
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using Habitat.Framework.Indexing.Infrastructure;
  using Habitat.Framework.Indexing.Models;
  using Sitecore;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.Linq;
  using Sitecore.ContentSearch.Linq.Utilities;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data;
  using Sitecore.Data.Items;

  public class SearchService
  {
    public ISearchSettings Settings { get; set; }

    public SearchService(ISearchSettings settings)
    {
      //TODO: Be more intelligent, read from Site settings
      this.IndexName = "sitecore_master_index";
      this.Settings = settings;
    }


    public ISearchResults Search(IQuery query)
    {
      using (var context = ContentSearchManager.GetIndex(this.IndexName).CreateSearchContext())
      {
        var root = this.Settings.Root;
        var queryable = context.GetQueryable<SearchResultItem>();
        queryable = SetQueryRoot(queryable, root);
        queryable = this.FilterOnPresentationOnly(queryable);
        queryable = FilterOnLanguage(queryable);
        queryable = this.FilterOnTemplates(queryable);
        queryable = this.AddContentPredicates(queryable, query);
        queryable = AddFacets(queryable);
        if (query.IndexOfFirstResult > 0)
        {
          queryable = queryable.Skip(query.IndexOfFirstResult);
        }
        if (query.NoOfResults > 0)
        {
          queryable = queryable.Take(query.NoOfResults);
        }
        var results = queryable.GetResults();

        return SearchResultsRepository.Create(results, query);
      }
    }

    private IQueryable<SearchResultItem> FilterOnTemplates(IQueryable<SearchResultItem> queryable)
    {
      var indexedItemPredicate = GetPredicateForItemDerivesFromIndexedItem();
      var contentTemplatePredicates = this.GetPredicatesForContentTemplates();
      return queryable.Cast<IndexedItem>().Where(indexedItemPredicate.And(contentTemplatePredicates));
    }

    private Expression<Func<IndexedItem, bool>> GetPredicatesForContentTemplates()
    {
      var contentTemplatePredicates = PredicateBuilder.False<IndexedItem>();
      foreach (var provider in IndexContentProviderRepository.All)
      {
        contentTemplatePredicates = contentTemplatePredicates.Or(this.GetTemplatePredicates(provider.SupportedTemplates));
      }
      return contentTemplatePredicates;
    }

    private static Expression<Func<IndexedItem, bool>> GetPredicateForItemDerivesFromIndexedItem()
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

    private IQueryable<SearchResultItem> FilterOnPresentationOnly(IQueryable<SearchResultItem> queryable)
    {
      return queryable.Cast<IndexedItem>().Where(i => i.HasPresentation && i.ShowInSearchResults);
    }

    private static IQueryable<SearchResultItem> AddFacets(IQueryable<SearchResultItem> queryable)
    {
      queryable = queryable.FacetOn(item => item.TemplateName);
      return queryable;
    }

    private static IQueryable<SearchResultItem> FilterOnLanguage(IQueryable<SearchResultItem> queryable)
    {
      queryable = queryable.Filter(item => item.Language == Context.Language.Name);
      return queryable;
    }

    private static IQueryable<SearchResultItem> SetQueryRoot(IQueryable<SearchResultItem> queryable, Item root)
    {
      queryable = queryable.Where(item => item.Path.StartsWith(root.Paths.FullPath));
      return queryable;
    }

    private IQueryable<SearchResultItem> AddContentPredicates(IQueryable<SearchResultItem> queryable, IQuery query)
    {
      var contentPredicates = PredicateBuilder.False<SearchResultItem>();
      foreach (var provider in IndexContentProviderRepository.All)
      {
        contentPredicates = contentPredicates.Or(provider.GetQueryPredicate(query));
      }
      return queryable.Where(contentPredicates);
    }

    public string IndexName { get; }
  }
}