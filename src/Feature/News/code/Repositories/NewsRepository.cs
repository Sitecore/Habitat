﻿namespace Sitecore.Feature.News.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.Indexing.Repositories;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class NewsRepository : INewsRepository
  {
    public Item ContextItem { get; set; }

    private readonly ISearchServiceRepository searchServiceRepository;

    public NewsRepository(Item contextItem) : this(contextItem, new SearchServiceRepository(new SearchSettingsBase { Templates = new[] { Templates.NewsArticle.ID } }))
    {
    }

    public NewsRepository(Item contextItem, ISearchServiceRepository searchServiceRepository)
    {
      if (contextItem == null)
      {
        throw new ArgumentNullException(nameof(contextItem));
      }
      if (!contextItem.IsDerived(Templates.NewsFolder.ID))
      {
        throw new ArgumentException("Item must derive from NewsFolder", nameof(contextItem));
      }
      this.ContextItem = contextItem;
      this.searchServiceRepository = searchServiceRepository;
    }

    public IEnumerable<Item> Get()
    {
      var searchService = this.searchServiceRepository.Get();
      searchService.Settings.Root = this.ContextItem;
      //TODO: Refactor for scalability
      var results = searchService.FindAll();
      return results.Results.Select(x => x.Item).OrderByDescending(i => i[Templates.NewsArticle.Fields.Date]);
    }

    public IEnumerable<Item> GetLatestNews(int count)
    {
      //TODO: Refactor for scalability
      return this.Get().Take(count);
    }
  }
}