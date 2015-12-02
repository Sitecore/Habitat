namespace Sitecore.Feature.Search.Repositories
{
  using System.Web;
  using Sitecore;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Feature.Search.Models;
  using Sitecore.Framework.SitecoreExtensions.Extensions;
  using Sitecore.Mvc.Presentation;

  public class SearchSettingsRepository : ISearchSettingsRepository
  {
    public virtual SearchSettings Get(string query)
    {
      if (query == null)
      {
        query = HttpContext.Current == null ? "" : HttpContext.Current.Request["query"];
      }

      var configurationItem = RenderingContext.Current.Rendering.Item;
      if (configurationItem == null || !configurationItem.IsDerived(Templates.SearchResults.ID))
      {
        return null;
      }

      return new SearchSettings
             {
               ConfigurationItem = configurationItem,
               Query = query,
               SearchBoxTitle = configurationItem[Templates.SearchResults.Fields.SearchBoxTitle],
               SearchResultsUrl = configurationItem.Url(),
               Root = GetRootItem(configurationItem)
             };
    }

    private  Item GetRootItem(Item configurationItem)
    {
      Item rootItem = null;
      if (configurationItem.Fields[Templates.SearchResults.Fields.Root].HasValue)
      {
        rootItem = ((ReferenceField)configurationItem.Fields[Templates.SearchResults.Fields.Root]).TargetItem;
      }
      return rootItem ?? Context.Site.GetRootItem();
    }

    public SearchSettings Get()
    {
      return this.Get(null);
    }
  }
}