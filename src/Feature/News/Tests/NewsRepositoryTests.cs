namespace Sitecore.Feature.News.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.Feature.News.Repositories;
  using Sitecore.Feature.News.Tests.Extensions;
  using Sitecore.Foundation.Indexing;
  using Sitecore.Rules.Conditions.ItemConditions;
  using Xunit;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Sitecore.ContentSearch.SearchTypes;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.Indexing.Repositories;
  using Sitecore.Search;

  public class NewsRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Get_ReturnsListOfNews([Frozen] ISearchServiceRepository searchServiceRepository, [Frozen] ISearchSettingsRepository searchSettingsRepository, string itemName, [Substitute] SearchService searchService)
    {
      var id = ID.NewID;
      searchServiceRepository.Get().Returns(searchService);
      var db = new Db
      {
        new DbItem(itemName, id, Templates.NewsFolder.ID)
      };
      var contextItem = db.GetItem(id);
      var repository = new NewsRepository(contextItem, searchServiceRepository);
      var news = repository.Get();
      news.Should().As<IEnumerable<Item>>();
    }

    [Theory]
    [AutoDbData]
    public void Constructor_NullAs1Parameter_ThrowArgumentNullException([Frozen] ISearchServiceRepository searchServiceRepository, [Frozen] ISearchSettingsRepository searchSettingsRepository)
    {
      Action act = () => new NewsRepository(null);
      act.ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [AutoDbData]
    public void Constructor_ItemNotDerivedFromNewsFolterTemplate_ThrowArgumentNullException([Frozen] ISearchServiceRepository searchServiceRepository, [Frozen] ISearchSettingsRepository searchSettingsRepository, Item contextItem)
    {
      Action act = () => new NewsRepository(contextItem, searchServiceRepository);
      act.ShouldThrow<ArgumentException>();
    }

    [Theory]
    [AutoDbData]
    public void GetLatestNews_IntegerAs1Parameter_ReturnsNumberOfNewsEquelToParameterValue([Frozen] ISearchServiceRepository searchServiceRepository, [Frozen] ISearchSettingsRepository searchSettingsRepository, string itemName, [Substitute] SearchService searchService, ISearchResults searchResults, List<Item> collection)
    {
      var id = ID.NewID;
      searchResults.Results.Returns(collection.Select(x=>new Foundation.Indexing.Models.SearchResult(x)));
      searchService.FindAll().Returns(searchResults);
      searchServiceRepository.Get().Returns(searchService);
      var db = new Db
      {
        new DbItem(itemName, id, Templates.NewsFolder.ID)
      };
      var contextItem = db.GetItem(id);
      var repository = new NewsRepository(contextItem, searchServiceRepository);
      var news = repository.GetLatestNews(1);
      news.Count().ShouldBeEquivalentTo(1);
    }
  }
}
