namespace Sitecore.Foundation.SitecoreExtensions.Tests.Renderings
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Abstractions;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.Linq.Common;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Pipelines;
  using Sitecore.Foundation.SitecoreExtensions.Rendering;
  using Sitecore.Mvc.Presentation;
  using Sitecore.Pipelines.GetRenderingDatasource;
  using UnitTests.Common.Attributes;
  using Xunit;

  public class QueryableDatasourceRenderingModelTests
  {
    [Theory]
    [AutoDbData]
    public void Items_IndexMatchDb_ReturnsAllItems([ResolvePipeline("getRenderingDatasource")] IPipelineProcessor processor, [Content] DbItem[] contentItems, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, string indexName, [Content] Item renderingItem)
    {
      //arrange
      var results = GetResults(contentItems);

      InitIndexes(index, searchProvider, indexName, results);

      var rendering = new Rendering
      {
        DataSource = "ds",
        RenderingItem = new RenderingItem(renderingItem)
      };
      var renderingModel = new QueryableDatasourceRenderingModel();
      renderingModel.Initialize(rendering);


      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(contentItems.Length);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }


    [Theory]
    [AutoDbData]
    public void Initialize_TemplateResolved_DatasourceTemplateShouldBeSet([Content] DbTemplate templateItem, [ResolvePipeline("getRenderingDatasource")] IPipelineProcessor processor, [Content] Item renderingItem)
    {
      //arrange
      var item = renderingItem.Database.GetItem(templateItem.ID);
      processor.WhenForAnyArgs(x => x.Process(Arg.Any<GetRenderingDatasourceArgs>())).Do(x => x.Arg<GetRenderingDatasourceArgs>().Prototype = item);


      var rendering = new Rendering
      {
        DataSource = "ds",
        RenderingItem = new RenderingItem(renderingItem)
      };
      var renderingModel = new QueryableDatasourceRenderingModel();
      //act
      renderingModel.Initialize(rendering);


      //assert
      renderingModel.DatasourceTemplate.Should().NotBeNull();
      renderingModel.DatasourceTemplate.ID.Should().Be(templateItem.ID);
    }


    [Theory]
    [AutoDbData]
    public void Items_ItemTemplateSet_FiltersByTemplateId(Db db, [Content] DbTemplate templateItem, [ResolvePipeline("getRenderingDatasource")] IPipelineProcessor processor, [Content] DbItem[] contentItems, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, string indexName, [Content] Item renderingItem)
    {
      //arrange
      var dbItem = new DbItem("templated", ID.NewID, templateItem.ID);
      db.Add(dbItem);
      var dbItems = contentItems.ToList();
      dbItems.Add(dbItem);

      var prototype = db.GetItem(templateItem.ID);
      processor.WhenForAnyArgs(x => x.Process(Arg.Any<GetRenderingDatasourceArgs>())).Do(x => { x.Arg<GetRenderingDatasourceArgs>().Prototype = prototype; });
      var results = GetResults(dbItems);

      InitIndexes(index, searchProvider, indexName, results);

      var rendering = new Rendering
      {
        DataSource = "ds",
        RenderingItem = new RenderingItem(renderingItem)
      };
      var renderingModel = new QueryableDatasourceRenderingModel();

      renderingModel.Initialize(rendering);


      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(1);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }


    [Theory]
    [AutoDbData]
    public void Items_IndexHaveNonexistentItems_ReturnsExistentItems([Content] DbItem[] contentItems, [ResolvePipeline("getRenderingDatasource")] IPipelineProcessor processor, DbItem brokenItem, List<DbItem> indexedItems, ISearchIndex index, string indexName, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem)
    {
      //arrange
      indexedItems.AddRange(contentItems);
      indexedItems.Add(brokenItem);


      var results = GetResults(indexedItems);

      InitIndexes(index, searchProvider, indexName, results);

      var rendering = new Rendering
      {
        DataSource = "ds",
        RenderingItem = new RenderingItem(renderingItem)
      };
      var renderingModel = new QueryableDatasourceRenderingModel();
      renderingModel.Initialize(rendering);

      //act
      var items = renderingModel.Items;


      //assert
      items.Count().Should().Be(contentItems.Length);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }


    [Theory]
    [AutoDbData]
    public void Items_IndexEmpty_ReturnsEmptyCollection([ResolvePipeline("getRenderingDatasource")] IPipelineProcessor processor, List<DbItem> indexedItems, ISearchIndex index, string indexName, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem)
    {
      //arrange
      InitIndexes(index, searchProvider, indexName, new List<SearchResultItem>().AsQueryable());

      var rendering = new Rendering
      {
        DataSource = "ds",
        RenderingItem = new RenderingItem(renderingItem)
      };
      var renderingModel = new QueryableDatasourceRenderingModel();
      renderingModel.Initialize(rendering);

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(0);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }

    [Theory]
    [AutoDbData]
    public void Items_EmptyDatasource_ReturnsEmptyCollection([ResolvePipeline("getRenderingDatasource")] IPipelineProcessor processor, List<DbItem> indexedItems, ISearchIndex index, string indexName, [Content] Item renderingItem)
    {
      //arrange

      InitIndexes(index, null, indexName, new List<SearchResultItem>().AsQueryable());

      var rendering = new Rendering
      {
        RenderingItem = new RenderingItem(renderingItem)
      };
      var renderingModel = new QueryableDatasourceRenderingModel();
      renderingModel.Initialize(rendering);


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
        searchResultItem.Templates = new List<string>
        {
          IdHelper.NormalizeGuid(item.TemplateID)
        };
        var dbItem = Context.Database.GetItem(item.ID);
        searchResultItem.GetItem().Returns(dbItem);
        list.Add(searchResultItem);
      }
      return list.AsQueryable();
    }

    private static void InitIndexes(ISearchIndex index, SearchProvider searchProvider, string indexName, IQueryable<SearchResultItem> results)
    {
      searchProvider?.GetContextIndexName(Arg.Any<IIndexable>(), Arg.Any<ICorePipeline>()).Returns(indexName);
      index.CreateSearchContext().GetQueryable<SearchResultItem>(Arg.Any<IExecutionContext[]>()).Returns(results);
      ContentSearchManager.SearchConfiguration.Indexes.Clear();
      ContentSearchManager.SearchConfiguration.Indexes.Add(indexName, index);
    }
  }
}