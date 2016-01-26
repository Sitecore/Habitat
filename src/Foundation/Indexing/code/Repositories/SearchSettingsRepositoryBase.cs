﻿namespace Sitecore.Foundation.Indexing.Repositories
{
  using Sitecore.Feature.News.Repositories;
  using Sitecore.Foundation.Indexing.Models;

  public class SearchSettingsRepositoryBase : ISearchSettingsRepository
  {
    public virtual ISearchSettings Get()
    {
      return new SearchSettingsBase();
    }
  }
}