namespace Habitat.Search.Models
{
  using System.Web;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Mvc.Presentation;

  internal class SearchSettingsRepository
  {
    public static SearchSettings Get(string query)
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

    public static SearchSettings Get()
    {
      return Get(null);
    }

    private static Item GetRootItem(Item configurationItem)
    {
      Item rootItem = null;
      if (configurationItem.Fields[Templates.SearchResults.Fields.Root].HasValue)
      {
        rootItem = ((ReferenceField)configurationItem.Fields[Templates.SearchResults.Fields.Root]).TargetItem;
      }
      return rootItem ?? (Context.Site.GetRoot());
    }
  }
}