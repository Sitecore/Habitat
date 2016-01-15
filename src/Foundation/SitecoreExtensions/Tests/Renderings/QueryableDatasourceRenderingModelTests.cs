namespace Sitecore.Foundation.SitecoreExtensions.Tests.Renderings
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using NSubstitute.Extensions;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Abstractions;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.Linq.Common;
  using Sitecore.ContentSearch.Pipelines.GetContextIndex;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Pipelines;
  using Sitecore.Foundation.SitecoreExtensions.Rendering;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Presentation;
  using Sitecore.Pipelines;
  using Sitecore.Pipelines.GetRenderingDatasource;
  using UnitTests.Common.Attributes;
  using UnitTests.Common.Pipelines;
  using Xunit;

  public class QueryableDatasourceRenderingModelTests
  {

    public class FakeDatasourceResolverPipeline : IPipelineProcessor
    {
      public Item Item { get; set; }
      public void Process(PipelineArgs args)
      {
        var castedArgs = args as GetRenderingDatasourceArgs;
        if (castedArgs != null)
          castedArgs.Prototype = Item;
      }
    }


    [Theory]
    [AutoDbData]
    public void Items_DifferentItemLanguageExists_ReturnsOnlyContextLanguage([Content] DbItem[] contentItems, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem)
    {
      //arrange
      var results = GetResults(contentItems).ToArray();
      results.First().Language = "noncontext";

      InitIndexes(index, searchProvider, results.AsQueryable());
      var renderingModel = new QueryableDatasourceRenderingModel() { Rendering = new Rendering() { DataSource = "notEmpty" } };

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(contentItems.Length - 1);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);

    }


    [Theory]
    [AutoDbData]
    public void Items_NotLatestItemVersionExists_ReturnsOnlyLatestItems([Content] DbItem[] contentItems, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem)
    {
      //arrange
      var results = GetResults(contentItems).ToArray();
      results.First().IsLatestVersion = false;

      InitIndexes(index, searchProvider, results.AsQueryable());
      var renderingModel = new QueryableDatasourceRenderingModel() { Rendering = new Rendering() { DataSource = "notEmpty" } };

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(contentItems.Length-1);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);

    }


    [Theory]
    [AutoDbData]
    public void Items_IndexMatchDb_ReturnsAllItems([Content] DbItem[] contentItems, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem)
    {
      //arrange
      var results = GetResults(contentItems);

      InitIndexes(index, searchProvider, results);
      var renderingModel = new QueryableDatasourceRenderingModel() { Rendering = new Rendering() { DataSource = "notEmpty" } };

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(contentItems.Length);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);

    }


    [Theory]
    [AutoDbData]
    public void Initialize_TemplateResolved_DatasourceTemplateShouldBeSet([Content] DbTemplate templateItem, [ResolvePipeline("getRenderingDatasource")] FakeDatasourceResolverPipeline processor, [Content] Item renderingItem)
    {
      //arrange
      processor.Item = renderingItem.Database.GetItem(templateItem.ID);
      var rendering = new Rendering
      {
        DataSource = "ds",
        RenderingItem = new RenderingItem(renderingItem)
        
      };
      ContextService.Get().Push(new PageContext());
      PageContext.Current.Item = renderingItem;
      var renderingModel = new QueryableDatasourceRenderingModel();
      //act
      renderingModel.Initialize(rendering);


      //assert
      renderingModel.DatasourceTemplate.Should().NotBeNull();
      renderingModel.DatasourceTemplate.ID.Should().Be(templateItem.ID);
    }


    [Theory]
    [AutoDbData]
    public void Items_ItemTemplateSet_FiltersByTemplateId(Db db, [Content] DbTemplate templateItem,  [Content] DbItem[] contentItems, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, string indexName, [Content] Item renderingItem)
    {
      //arrange
      var dbItem = new DbItem("templated", ID.NewID, templateItem.ID);
      db.Add(dbItem);
      var dbItems = contentItems.ToList();
      dbItems.Add(dbItem);
      var results = GetResults(dbItems);

      InitIndexes(index, searchProvider,  results);

      
      var renderingModel = new QueryableDatasourceRenderingModel() {Rendering = new Rendering() {DataSource = "notEmpty"} };
      renderingModel.DatasourceTemplate = db.GetItem(templateItem.ID);

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(1);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }


    [Theory]
    [AutoDbData]
    public void Items_IndexHaveNonexistentItems_ReturnsExistentItems([Content] DbItem[] contentItems, DbItem brokenItem, List<DbItem> indexedItems, ISearchIndex index, string indexName, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem)
    {
      //arrange
      indexedItems.AddRange(contentItems);
      indexedItems.Add(brokenItem);

      var results = GetResults(indexedItems);

      InitIndexes(index, searchProvider, results);
      var renderingModel = new QueryableDatasourceRenderingModel() { Rendering = new Rendering() { DataSource = "notEmpty" } };

      //act
      var items = renderingModel.Items.ToArray();


      //assert
      items.Count().Should().Be(contentItems.Length);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }


    [Theory]
    [AutoDbData]
    public void Items_IndexEmpty_ReturnsEmptyCollection([ResolvePipeline("getRenderingDatasource")] EmptyPipeline processor, List<DbItem> indexedItems,  ISearchIndex index, string indexName, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem)
    {
      //arrange
      InitIndexes(index, searchProvider,  new List<SearchResultItem>().AsQueryable());
      var renderingModel = new QueryableDatasourceRenderingModel() { Rendering = new Rendering() { DataSource = "notEmpty" } };

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(0);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);

    }

    [Theory]
    [AutoDbData]
    public void Items_EmptyDatasource_ReturnsEmptyCollection([ResolvePipeline("getRenderingDatasource")] EmptyPipeline processor, List<DbItem> indexedItems,SearchProvider searchProvider, ISearchIndex index, string indexName, [Content] Item renderingItem)
    {
      //arrange

      InitIndexes(index, searchProvider, new List<SearchResultItem>().AsQueryable());
      var renderingModel = new QueryableDatasourceRenderingModel() { Rendering = new Rendering()};

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(0);
      index.CreateSearchContext().DidNotReceiveWithAnyArgs();
 
    }


    private static IQueryable<SearchResult> GetResults(IEnumerable<DbItem> contentItems)
    {
      var list = new List<SearchResult>();

      foreach (var item in contentItems)
      {
        var searchResultItem = Substitute.For<SearchResult>();
        searchResultItem.Templates = new List<string> { IdHelper.NormalizeGuid(item.TemplateID) };
        searchResultItem.IsLatestVersion = true;
        searchResultItem.Language = Context.Language.Name;
        var dbItem = Context.Database.GetItem(item.ID);
        searchResultItem.GetItem().Returns(dbItem);
        list.Add(searchResultItem);
      }
      return list.AsQueryable();
    }

    private static void InitIndexes(ISearchIndex index, SearchProvider searchProvider,  IQueryable<SearchResultItem> results)
    {
      ContentSearchManager.SearchConfiguration.Indexes.Clear();
      searchProvider?.GetContextIndexName(Arg.Any<IIndexable>(), Arg.Any<ICorePipeline>()).Returns("CustomIndexName");

      var providerSearchContext = Substitute.For<IProviderSearchContext>();
      index.ReturnsForAll(providerSearchContext);
      providerSearchContext.GetQueryable<SearchResultItem>(Arg.Any<IExecutionContext[]>()).Returns(results);
      ContentSearchManager.SearchConfiguration.Indexes.Add("CustomIndexName", index);
    }
  }
}