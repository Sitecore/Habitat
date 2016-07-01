namespace Sitecore.Foundation.Indexing.Rendering
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Mvc.Presentation;
  using Sitecore.Pipelines;
  using Sitecore.Pipelines.GetRenderingDatasource;

  public class QueryableDatasourceRenderingModel : RenderingModel
  {
    private readonly IRenderingPropertiesRepository renderingPropertiesRepository;
    private IEnumerable<Item> items;
    private const int DefaultMaxResults = 10;

    public QueryableDatasourceRenderingSettings Settings => this.renderingPropertiesRepository.Get<QueryableDatasourceRenderingSettings>();

    public QueryableDatasourceRenderingModel() : this(new RenderingPropertiesRepository())
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
      this.ResolveDatasourceTemplate(rendering);
      this.ParseRenderingDataSource(rendering);
    }

    public virtual IEnumerable<Item> Items => this.items ?? (this.items = this.GetItemsFromDatasourceQuery().ToArray());

    private IEnumerable<Item> GetItemsFromDatasourceQuery()
    {
      if (string.IsNullOrEmpty(this.DatasourceString))
      {
        return Enumerable.Empty<Item>();
      }

      using (var providerSearchContext = ContentSearchManager.GetIndex((SitecoreIndexableItem)Context.Item).CreateSearchContext())
      {
        var query = LinqHelper.CreateQuery<SearchResultItem>(providerSearchContext, SearchStringModel.ParseDatasourceString(this.DatasourceString));
        var searchResultItems = query.Cast<IndexedItem>();
        if (this.DatasourceTemplate != null)
        {
          var templateId = IdHelper.NormalizeGuid(this.DatasourceTemplate.ID);
          searchResultItems = searchResultItems.Where(x => x.AllTemplates.Contains(templateId));
        }
        return searchResultItems.Where(x => x.Language == Context.Language.Name).Where(x => x.IsLatestVersion).Where(x => !x.Paths.Contains(ItemIDs.TemplateRoot)).Where(x => !x.Name.Equals(Sitecore.Constants.StandardValuesItemName, StringComparison.InvariantCultureIgnoreCase)).Take(this.MaxResults).Select(current => current != null ? current.GetItem() : null).ToArray().Where(item => item != null);
      }
    }

    private int MaxResults => this.Settings.SearchResultsLimit > 0 ? this.Settings.SearchResultsLimit : DefaultMaxResults;

    private void ParseRenderingDataSource(Rendering rendering)
    {
      var dataSource = rendering.DataSource;
      if (string.IsNullOrWhiteSpace(dataSource))
      {
        dataSource = "+location:" + this.Rendering.Item.ID;
      }
      if (MainUtil.IsID(dataSource))
      {
        dataSource = "+location:" + dataSource;
      }
      this.DatasourceString = dataSource;
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
        ContextItemPath = rendering.Item?.Paths.FullPath ?? this.PageItem.Paths.FullPath
      };
      CorePipeline.Run("getRenderingDatasource", getRenderingDatasourceArgs);

      this.DatasourceTemplate = getRenderingDatasourceArgs.Prototype;
    }
  }
}