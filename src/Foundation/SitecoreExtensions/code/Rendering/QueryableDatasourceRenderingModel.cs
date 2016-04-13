namespace Sitecore.Foundation.SitecoreExtensions.Rendering
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Model;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Mvc.Presentation;
  using Sitecore.Pipelines;
  using Sitecore.Pipelines.GetRenderingDatasource;

  public class QueryableDatasourceRenderingModel : RenderingModel
  {
    private readonly IRenderingPropertiesRepository renderingPropertiesRepository;
    private IEnumerable<Item> items;
    private const int DefaultMaxResults = 10;

    public QueryableDatasourceRenderingSettings Settings => renderingPropertiesRepository.Get<QueryableDatasourceRenderingSettings>();

    public QueryableDatasourceRenderingModel():this(new RenderingPropertiesRepository())
    {
      
    }
    public QueryableDatasourceRenderingModel(IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      this.renderingPropertiesRepository = renderingPropertiesRepository;
    }

    public Item DatasourceTemplate { get; set; }

    public override void Initialize(Rendering rendering)
    {
      base.Initialize(rendering);
      ResolveDatasourceTemplate(rendering);
      ParseRenderingDataSource(rendering);
    }

    public virtual IEnumerable<Item> Items => items ?? (items = GetItemsFromDatasourceQuery().ToArray());

    private IEnumerable<Item> GetItemsFromDatasourceQuery()
    {
      if (string.IsNullOrEmpty(DatasourceString))
      {
        return Enumerable.Empty<Item>();
      }

      using (var providerSearchContext = ContentSearchManager.GetIndex((SitecoreIndexableItem)Context.Item).CreateSearchContext())
      {
        var query = LinqHelper.CreateQuery<SearchResultItem>(providerSearchContext, SearchStringModel.ParseDatasourceString(DatasourceString));
        var searchResultItems = query.Cast<SearchResult>();
        if (DatasourceTemplate != null)
        {
          var templateId = IdHelper.NormalizeGuid(DatasourceTemplate.ID);
          searchResultItems = searchResultItems.Where(x => x.Templates.Contains(templateId));
        }
        return searchResultItems.Where(x => x.Language == Context.Language.Name).Where(x => x.IsLatestVersion).Where(x => !x.Paths.Contains(ItemIDs.TemplateRoot)).Where(x => !x.Name.Equals(Sitecore.Constants.StandardValuesItemName, StringComparison.InvariantCultureIgnoreCase)).Take(MaxResults).Select(current => current != null ? current.GetItem() : null).ToArray().Where(item => item != null);
      }
    }

    private int MaxResults => Settings.SearchResultsLimit > 0 ? Settings.SearchResultsLimit : DefaultMaxResults;

    private void ParseRenderingDataSource(Rendering rendering)
    {
      var dataSource = rendering.DataSource;
      if (string.IsNullOrWhiteSpace(dataSource))
      {
        dataSource = "+location:" + Rendering.Item.ID;
      }
      if (MainUtil.IsID(dataSource))
      {
        dataSource = "+location:" + dataSource;
      }
      this.DatasourceString =  dataSource;
    }

    public string DatasourceString { get; set; }


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