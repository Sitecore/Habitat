namespace Habitat.Search.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Habitat.Accounts.Tests.Extensions;
  using Habitat.Search.Models;
  using Habitat.Search.Repositories;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
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
  }
}
