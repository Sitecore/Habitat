using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.News.Repositories
{
  using Sitecore.Data;

  public class SearchSettingsRepository : ISearchSettingsRepository
  {
    public SearchSettings Get()
    {
      var settings = new SearchSettings();
      settings.Tempaltes = new List<ID>
      {
        Templates.NewsArticle.ID
      };

      return settings;
    }
  }
}