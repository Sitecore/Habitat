namespace Sitecore.Foundation.SitecoreExtensions.Rendering
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.Linq.Utilities;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Mvc.Presentation;
  using Sitecore.Pipelines;
  using Sitecore.Pipelines.GetRenderingDatasource;


  public class QueryableDatasourceRenderingModel : RenderingModel
  {
  
    public Item DatasourceTemplate { get; set; }

    public override void Initialize(Rendering rendering)
    {
      base.Initialize(rendering);
      ResolveDatasource(rendering);
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

          if (DatasourceTemplate!=null)
          {
            var templateId = IdHelper.NormalizeGuid(DatasourceTemplate.ID);
            items = items.Cast<SearchResult>().Where(x => x.Templates.Contains(templateId));
          }
          return items.Select(current => current != null ? current.GetItem() : null).ToArray().Where(item => item != null);
        }
      }
    }

   

    private void ResolveDatasource(Rendering rendering)
    {
      var getRenderingDatasourceArgs = new GetRenderingDatasourceArgs(rendering.RenderingItem.InnerItem)
      {
        FallbackDatasourceRoots = new List<Item>
        {
          Context.Database.GetRootItem()
        },
        ContentLanguage = rendering.Item?.Language,
        ContextItemPath = (rendering.Item != null) ? rendering.Item.Paths.FullPath : string.Empty
      };
      CorePipeline.Run("getRenderingDatasource", getRenderingDatasourceArgs);

      DatasourceTemplate = getRenderingDatasourceArgs.Prototype;
    }
  }
}