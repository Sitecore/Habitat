using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.News.Repositories
{
  using Sitecore.ContentSearch;
  using Sitecore.Data;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.Indexing.Repositories;

  public class SearchSettingsRepository : SearchSettingsRepositoryBase
  {
    public override ISearchSettings Get()
    {
      var settings = new SearchSettingsBase();
      settings.Templates = new List<ID>
      {
        Templates.NewsArticle.ID
      };

      return settings;
    }
  }
}