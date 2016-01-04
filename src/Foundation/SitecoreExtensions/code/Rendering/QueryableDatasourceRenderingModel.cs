namespace Sitecore.Foundation.SitecoreExtensions.Rendering
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data.Items;
  using Sitecore.Mvc.Presentation;

  public class QueryableDatasourceRenderingModel : RenderingModel
  {
    public string IndexName => "sitecore_master_index";

    public virtual IEnumerable<Item> Items
    {
      get
      {
        var dataSource = Rendering.DataSource;

        var result = new List<Item>();

        if (dataSource != null)
        {
          using (var providerSearchContext = ContentSearchManager.GetIndex(IndexName).CreateSearchContext())
          {
            var list = LinqHelper.CreateQuery<SearchResultItem>(providerSearchContext, SearchStringModel.ParseDatasourceString(dataSource));
            result.AddRange(list.Select(current => current != null ? current.GetItem() : null).ToArray().Where(item => item != null));
          }
        }
        return result;
      }
    }
  }
}