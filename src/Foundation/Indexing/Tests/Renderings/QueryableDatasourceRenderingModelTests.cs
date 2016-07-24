namespace Sitecore.Foundation.Indexing.Tests.Renderings
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using NSubstitute.Extensions;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Abstractions;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.Linq.Common;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.ContentSearch.Utilities;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.Indexing.Rendering;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Presentation;
  using Sitecore.Pipelines;
  using Sitecore.Pipelines.GetRenderingDatasource;
  using Xunit;

  public class QueryableDatasourceRenderingModelTests
  {
    [Theory]
    [AutoDbData]
    public void Items_DifferentItemLanguageExists_ReturnsOnlyContextLanguage([Content] DbItem[] contentItems, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      //arrange
      var results = GetResults(contentItems).ToArray();
      results.First().Language = "noncontext";
      renderingPropertiesRepository.Get<QueryableDatasourceRenderingSettings>()
        .Returns(new QueryableDatasourceRenderingSettings
        {
          SearchResultsLimit = 10
        });

      InitIndexes(index, searchProvider, results.AsQueryable());
      var renderingModel = new QueryableDatasourceRenderingModel(renderingPropertiesRepository)
      {
        DatasourceString = "notEmpty"
      };

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(contentItems.Length - 1);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }


    [Theory]
    [AutoDbData]
    public void Items_NotLatestItemVersionExists_ReturnsOnlyLatestItems([Content] DbItem[] contentItems, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      //arrange
      var results = GetResults(contentItems).ToArray();
      results.First().IsLatestVersion = false;
      renderingPropertiesRepository.Get<QueryableDatasourceRenderingSettings>()
        .Returns(new QueryableDatasourceRenderingSettings
        {
          SearchResultsLimit = 10
        });

      InitIndexes(index, searchProvider, results.AsQueryable());
      var renderingModel = new QueryableDatasourceRenderingModel(renderingPropertiesRepository)
      {
        DatasourceString = "notEmpty"
      };

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(contentItems.Length - 1);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }


    [Theory]
    [AutoDbData]
    public void Items_IndexMatchDb_ReturnsAllItems([Content] DbItem[] contentItems, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      //arrange
      var results = GetResults(contentItems);
      renderingPropertiesRepository.Get<QueryableDatasourceRenderingSettings>()
        .Returns(new QueryableDatasourceRenderingSettings
        {
          SearchResultsLimit = 10
        });

      InitIndexes(index, searchProvider, results);
      var renderingModel = new QueryableDatasourceRenderingModel(renderingPropertiesRepository)
      {
        DatasourceString = "notEmpty"
      };

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(contentItems.Length);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }


    [Theory]
    [AutoDbData]
    public void Items_StandardValuesExistsInContentTree_IgnoresStandartValueByName(Db db, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      //arrange
      var id = ID.NewID;
      var dbItem = new DbItem(Sitecore.Constants.StandardValuesItemName, id);
      db.Add(dbItem);

      renderingPropertiesRepository.Get<QueryableDatasourceRenderingSettings>().Returns(new QueryableDatasourceRenderingSettings
      {
        SearchResultsLimit = 10
      });

      var results = GetResults(new List<DbItem>
      {
        dbItem
      });

      InitIndexes(index, searchProvider, results);
      var renderingModel = new QueryableDatasourceRenderingModel(renderingPropertiesRepository)
      {
        DatasourceString = "notEmpty"
      };

      //act
      var items = renderingModel.Items.ToArray();


      //assert
      items.Count().Should().Be(0);
    }


    [Theory]
    [AutoDbData]
    public void Initialize_TemplateResolved_DatasourceTemplateShouldBeSet([Content] DbTemplate templateItem, [Content] Item renderingItem,
      [Greedy] QueryableDatasourceRenderingModel renderingModel)
    {
      //arrange
      renderingItem = renderingItem.Database.GetItem(templateItem.ID);
      renderingModel.CorePipeline
        .When(cp => cp.Run("getRenderingDatasource", Arg.Is<GetRenderingDatasourceArgs>(a => a.RenderingItem == renderingItem)))
        .Do(ci => ci.Arg<GetRenderingDatasourceArgs>().Prototype = renderingItem);

      var rendering = new Rendering
      {
        DataSource = "ds",
        RenderingItem = new RenderingItem(renderingItem)
      };
      ContextService.Get().Push(new PageContext());
      PageContext.Current.Item = renderingItem;

      //act
      renderingModel.Initialize(rendering);

      //assert
      renderingModel.DatasourceTemplate.Should().NotBeNull();
      renderingModel.DatasourceTemplate.ID.Should().Be(templateItem.ID);
    }


    [Theory]
    [AutoDbData]
    public void Items_ItemTemplateSet_FiltersByTemplateId(Db db, [Content] DbTemplate templateItem, [Content] DbItem[] contentItems, ISearchIndex index, [ReplaceSearchProvider] SearchProvider searchProvider, string indexName, [Content] Item renderingItem, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      //arrange
      var dbItem = new DbItem("templated", ID.NewID, templateItem.ID);
      db.Add(dbItem);
      var dbItems = contentItems.ToList();
      dbItems.Add(dbItem);
      var results = GetResults(dbItems);
      renderingPropertiesRepository.Get<QueryableDatasourceRenderingSettings>()
        .Returns(new QueryableDatasourceRenderingSettings
        {
          SearchResultsLimit = 10
        });

      InitIndexes(index, searchProvider, results);


      var renderingModel = new QueryableDatasourceRenderingModel(renderingPropertiesRepository)
      {
        Rendering = new Rendering(),
        DatasourceString = "notEmpty",
        DatasourceTemplate = db.GetItem(templateItem.ID)
      };

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(1);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }


    [Theory]
    [AutoDbData]
    public void Items_IndexHaveNonexistentItems_ReturnsExistentItems([Content] DbItem[] contentItems, DbItem brokenItem,
      List<DbItem> indexedItems, ISearchIndex index, string indexName,
      [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      //arrange
      indexedItems.AddRange(contentItems);
      indexedItems.Add(brokenItem);
      renderingPropertiesRepository.Get<QueryableDatasourceRenderingSettings>()
        .Returns(new QueryableDatasourceRenderingSettings
        {
          SearchResultsLimit = 10
        });

      var results = GetResults(indexedItems);

      InitIndexes(index, searchProvider, results);
      var renderingModel = new QueryableDatasourceRenderingModel(renderingPropertiesRepository)
      {
        DatasourceString = "notEmpty"
      };

      //act
      var items = renderingModel.Items.ToArray();


      //assert
      items.Count().Should().Be(contentItems.Length);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }

    [Theory]
    [AutoDbData]
    public void Items_StandardValuesExists_IgnoresItemsUnderTemplates(Db db, ISearchIndex index,
      [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      //arrange
      var templateID = ID.NewID;
      db.Add(new DbTemplate("Sample", templateID));
      var stdValues = db.GetItem("/sitecore/templates/Sample").Add(Sitecore.Constants.StandardValuesItemName, new TemplateID(templateID));

      renderingPropertiesRepository.Get<QueryableDatasourceRenderingSettings>()
        .Returns(new QueryableDatasourceRenderingSettings
        {
          SearchResultsLimit = 10
        });

      var results = GetResults(new List<DbItem>
      {
        new DbItem(Sitecore.Constants.StandardValuesItemName, stdValues.ID)
      });

      InitIndexes(index, searchProvider, results);
      var renderingModel = new QueryableDatasourceRenderingModel(renderingPropertiesRepository)
      {
        DatasourceString = "notEmpty"
      };

      //act
      var items = renderingModel.Items.ToArray();


      //assert
      items.Count().Should().Be(0);
    }


    [Theory]
    [AutoDbData]
    public void Items_IndexEmpty_ReturnsEmptyCollection(List<DbItem> indexedItems, ISearchIndex index, string indexName, [ReplaceSearchProvider] SearchProvider searchProvider, [Content] Item renderingItem, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      //arrange
      InitIndexes(index, searchProvider, new List<SearchResultItem>().AsQueryable());
      renderingPropertiesRepository.Get<QueryableDatasourceRenderingSettings>()
        .Returns(new QueryableDatasourceRenderingSettings
        {
          SearchResultsLimit = 10
        });

      var renderingModel = new QueryableDatasourceRenderingModel(renderingPropertiesRepository)
      {
        DatasourceString = "notEmpty"
      };

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(0);
      index.CreateSearchContext().ReceivedWithAnyArgs(1);
    }

    [Theory]
    [AutoDbData]
    public void Items_EmptyDatasource_ReturnsEmptyCollection(List<DbItem> indexedItems, SearchProvider searchProvider, ISearchIndex index, string indexName, [Content] Item renderingItem)
    {
      //arrange

      InitIndexes(index, searchProvider, new List<SearchResultItem>().AsQueryable());
      var renderingModel = new QueryableDatasourceRenderingModel();

      //act
      var items = renderingModel.Items;

      //assert
      items.Count().Should().Be(0);
      index.CreateSearchContext().DidNotReceiveWithAnyArgs();
    }


    [Theory]
    [AutoDbData]
    public void DatasourceString_EmptyDatasource_ContextItemAsLocationRoot([Content] Item renderingItem,
      [Greedy] QueryableDatasourceRenderingModel renderingModel)
    {
      //arrange
      ContextService.Get().Push(new PageContext());
      PageContext.Current.Item = renderingItem;

      //act
      renderingModel.Initialize(new Rendering
      {
        DataSource = string.Empty,
        RenderingItem = new RenderingItem(renderingItem)
      });

      //assert
      renderingModel.DatasourceString.Should().Be("+location:" + PageContext.Current.Item.ID);
    }

    [Theory]
    [AutoDbData]
    public void DatasourceString_IdAsDatasource_IDSetAsLocationRoot([Content] Item renderingItem,
      [Greedy] QueryableDatasourceRenderingModel renderingModel)
    {
      //arrange
      ContextService.Get().Push(new PageContext());
      PageContext.Current.Item = renderingItem;
      var dataSource = ID.NewID.ToString();

      //act
      renderingModel.Initialize(new Rendering
      {
        DataSource = dataSource,
        RenderingItem = new RenderingItem(renderingItem)
      });

      //assert
      renderingModel.DatasourceString.Should().Be("+location:" + dataSource);
    }

    [Fact]
    public void SutInstantiatesDefaultCorePipelineWrapper()
    {
      var sut = new QueryableDatasourceRenderingModel();
      sut.CorePipeline.Should().BeOfType<CorePipelineWrapper>();
    }

    [Theory, AutoDbData]
    public void SutInstantiatesCustomCorePipelineWrapper(
      IRenderingPropertiesRepository renderingRepository,
      ICorePipeline corePipeline)
    {
      var sut = new QueryableDatasourceRenderingModel(renderingRepository, corePipeline);
      sut.CorePipeline.Should().BeSameAs(corePipeline);
    }

    private static IQueryable<IndexedItem> GetResults(IEnumerable<DbItem> contentItems)
    {
      var list = new List<IndexedItem>();

      foreach (var item in contentItems)
      {
        var searchResultItem = Substitute.For<IndexedItem>();
        searchResultItem.AllTemplates = new List<string>
        {
          IdHelper.NormalizeGuid(item.TemplateID)
        };
        searchResultItem.IsLatestVersion = true;
        searchResultItem.Language = Context.Language.Name;
        searchResultItem.Name.Returns(item.Name);

        var dbItem = Context.Database.GetItem(item.ID);
        searchResultItem.GetItem().Returns(dbItem);
        searchResultItem.Paths.Returns(dbItem?.Paths.LongID.Split(new[]
        {
          '/'
        }, StringSplitOptions.RemoveEmptyEntries).Select(x => new ID(x)).ToArray() ?? new ID[0]);
        list.Add(searchResultItem);
      }
      return list.AsQueryable();
    }

    private static void InitIndexes(ISearchIndex index, SearchProvider searchProvider, IQueryable<SearchResultItem> results)
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