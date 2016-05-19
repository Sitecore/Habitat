namespace Sitecore.Feature.Search.Repositories
{
  using System.Web;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Feature.Search.Models;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Mvc.Presentation;

  public class SearchContextRepository : ISearchContextRepository
  {
    public virtual SearchContext Get(string query)
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

      return new SearchContext
             {
               ConfigurationItem = configurationItem,
               Query = query,
               SearchBoxTitle = configurationItem[Templates.SearchResults.Fields.SearchBoxTitle],
               SearchResultsUrl = configurationItem.Url(),
               Root = this.GetRootItem(configurationItem)
             };
    }

    private Item GetRootItem(Item configurationItem)
    {
      Item rootItem = null;
      if (configurationItem.FieldHasValue(Templates.SearchResults.Fields.Root))
      {
        rootItem = ((ReferenceField)configurationItem.Fields[Templates.SearchResults.Fields.Root]).TargetItem;
      }
      return rootItem ?? Context.Site.GetRootItem();
    }

    public SearchContext Get()
    {
      return this.Get(null);
    }
  }
}