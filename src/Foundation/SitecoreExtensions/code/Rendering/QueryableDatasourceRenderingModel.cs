namespace Sitecore.Foundation.SitecoreExtensions.Rendering
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Model;
  using Sitecore.Mvc.Presentation;
  using Sitecore.Pipelines;
  using Sitecore.Pipelines.GetRenderingDatasource;

  public class QueryableDatasourceRenderingModel : RenderingModel
  {
    public Item DatasourceTemplate { get; set; }

    public override void Initialize(Rendering rendering)
    {
      base.Initialize(rendering);
      ResolveDatasourceTemplate(rendering);
    }

    public virtual IEnumerable<Item> Items  
    {
      get
      {
        var dataSource = Rendering.DataSource;
        if (string.IsNullOrEmpty(dataSource))
        {
          return Enumerable.Empty<Item>();
        }

        using (var providerSearchContext = ContentSearchManager.GetIndex((SitecoreIndexableItem)Context.Item).CreateSearchContext())
        {
          var items = LinqHelper.CreateQuery<SearchResultItem>(providerSearchContext, SearchStringModel.ParseDatasourceString(dataSource));
          var searchResultItems = items.Cast<SearchResult>();
          if (DatasourceTemplate != null)
          {
            var templateId = IdHelper.NormalizeGuid(DatasourceTemplate.ID);
            searchResultItems = searchResultItems.Where(x => x.Templates.Contains(templateId));
          }
          return searchResultItems
            .Where(x => x.Language == Context.Language.Name)
            .Where(x => x.IsLatestVersion)
            .Select(current => current != null ? current.GetItem() : null)
            .ToArray()
            .Where(item => item != null);
        }
      }
    }


    private void ResolveDatasourceTemplate(Rendering rendering)
    {
      var getRenderingDatasourceArgs = new GetRenderingDatasourceArgs(rendering.RenderingItem.InnerItem)
      {
        FallbackDatasourceRoots = new List<Item>
        {
          Context.Database.GetRootItem()
        },
        ContentLanguage = rendering.Item?.Language,
        ContextItemPath = rendering.Item?.Paths.FullPath ?? PageItem.Paths.FullPath
      };
      CorePipeline.Run("getRenderingDatasource", getRenderingDatasourceArgs);

      DatasourceTemplate = getRenderingDatasourceArgs.Prototype;
    }
  }
}