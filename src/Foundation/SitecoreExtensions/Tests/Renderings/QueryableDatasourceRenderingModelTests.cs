namespace Sitecore.Foundation.SitecoreExtensions.Tests.Renderings
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.Linq.Common;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.SitecoreExtensions.Model;
  using Sitecore.Foundation.SitecoreExtensions.Rendering;
  using Sitecore.Foundation.SitecoreExtensions.Tests.Common;
  using Sitecore.Mvc.Presentation;
  using Xunit;

  public class QueryableDatasourceRenderingModelTests
  {
    [Theory]
    [AutoDbData]
    public void Items_IndexMatchDb_ReturnsAllItems([Content] DbItem[] contentItems, ISearchIndex index)
    {
      //arrange
      ContentSearchManager.SearchConfiguration.Indexes.Clear();
      var rendering = new Rendering() { DataSource = "ds" };
      var results = GetResults(contentItems);
      index.CreateSearchContext().GetQueryable<SearchResultItem>(Arg.Any<IExecutionContext[]>()).Returns(results);
      var renderingModel = new QueryableDatasourceRenderingModel();
      ContentSearchManager.SearchConfiguration.Indexes.Add(renderingModel.IndexName, index);
      renderingModel.Initialize(rendering);

      //act
      var items = renderingModel.Items;


      //assert
      items.Count().Should().Be(contentItems.Length);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }

    [Theory]
    [AutoDbData]
    public void Items_IndexHaveNonexistentItems_ReturnsExistentItems([Content] DbItem[] contentItems, DbItem brokenItem, List<DbItem> indexedItems, ISearchIndex index)
    {
      //arrange
      indexedItems.AddRange(contentItems);
      indexedItems.Add(brokenItem);

      ContentSearchManager.SearchConfiguration.Indexes.Clear();
      var rendering = new Rendering() { DataSource = "ds" };
      var results = GetResults(indexedItems);
      index.CreateSearchContext().GetQueryable<SearchResultItem>(Arg.Any<IExecutionContext[]>()).Returns(results);
      var renderingModel = new QueryableDatasourceRenderingModel();
      ContentSearchManager.SearchConfiguration.Indexes.Add(renderingModel.IndexName, index);
      renderingModel.Initialize(rendering);

      //act
      var items = renderingModel.Items;


      //assert
      items.Count().Should().Be(contentItems.Length);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }


    [Theory]
    [AutoDbData]
    public void Items_IndexEmpty_ReturnsEmptyCollection(List<DbItem> indexedItems, ISearchIndex index)
    {
      //arrange

      ContentSearchManager.SearchConfiguration.Indexes.Clear();
      var rendering = new Rendering() {DataSource = "ds"};
      index.CreateSearchContext().GetQueryable<SearchResultItem>(Arg.Any<IExecutionContext[]>()).Returns(new List<SearchResultItem>().AsQueryable());
      var renderingModel = new QueryableDatasourceRenderingModel();
      ContentSearchManager.SearchConfiguration.Indexes.Add(renderingModel.IndexName, index);
      renderingModel.Initialize(rendering);

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(0);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }

    [Theory]
    [AutoDbData]
    public void Items_EmptyDatasource_ReturnsEmptyCollection(List<DbItem> indexedItems, ISearchIndex index)
    {
      //arrange

      ContentSearchManager.SearchConfiguration.Indexes.Clear();
      var rendering = new Rendering() { };
      index.CreateSearchContext().GetQueryable<SearchResultItem>(Arg.Any<IExecutionContext[]>()).Returns(new List<SearchResultItem>().AsQueryable());
      var renderingModel = new QueryableDatasourceRenderingModel();
      ContentSearchManager.SearchConfiguration.Indexes.Add(renderingModel.IndexName, index);
      renderingModel.Initialize(rendering);

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(0);
      index.CreateSearchContext().DidNotReceiveWithAnyArgs();
    }


    private IQueryable<SearchResultItem> GetResults(IEnumerable<DbItem> contentItems)
    {
      var list = new List<SearchResultItem>();

      foreach (var item in contentItems)
      {
        var searchResultItem = Substitute.For<SearchResultItem>();
        var dbItem = Context.Database.GetItem(item.ID);
        searchResultItem.GetItem().Returns(dbItem);
        list.Add(searchResultItem);
      }
      return list.AsQueryable();
    }
  }
}