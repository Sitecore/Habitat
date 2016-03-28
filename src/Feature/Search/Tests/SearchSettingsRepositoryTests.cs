namespace Sitecore.Feature.Search.Tests
{
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.Feature.Search.Models;
  using Sitecore.Feature.Search.Repositories;
  using Sitecore.Feature.Search.Tests.Extensions;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Presentation;
  using Xunit;

  public class SearchSettingsRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldReturnSearchSettings(Item item)
    {
      var itemId = ID.NewID;
      var db = new Db
      {
        new DbItem("item", itemId, Templates.SearchResults.ID) { {Templates.SearchResults.Fields.Root, itemId.ToString()} }
      };
      var testItem = db.GetItem(itemId);
      var context = new RenderingContext();
      context.Rendering = new Rendering();
      context.Rendering.Item = testItem;
      ContextService.Get().Push(context);
      var repository = new SearchSettingsRepository();
      var searchSettings = repository.Get();
      searchSettings.Should().BeOfType<SearchSettings>();
    }

    [Theory]
    [AutoDbData]
    public void ShouldReturnNullIfNotSearchResult(Item item)
    {
      var context = new RenderingContext();
      context.Rendering = new Rendering();
      context.Rendering.Item = item;
      ContextService.Get().Push(context);
      var repository = new SearchSettingsRepository();
      var searchSettings = repository.Get();
      searchSettings.Should().BeNull();
    }
  }
}
