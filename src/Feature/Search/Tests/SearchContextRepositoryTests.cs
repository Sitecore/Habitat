namespace Sitecore.Feature.Search.Tests
{
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.Feature.Search.Models;
  using Sitecore.Feature.Search.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Presentation;
  using Xunit;

  public class SearchContextRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Get_ShouldReturnSearchContext(Item item)
    {
      var itemId = ID.NewID;
      var db = new Db
               {
                 new DbItem("item", itemId, Templates.SearchResults.ID)
                 {
                   {Templates.SearchResults.Fields.Root, itemId.ToString()}
                 }
               };
      var testItem = db.GetItem(itemId);
      var context = new RenderingContext
                    {
                      Rendering = new Rendering
                                  {
                                    Item = testItem
                                  }
                    };
      ContextService.Get().Push(context);
      var repository = new SearchContextRepository();
      var searchContext = repository.Get();
      searchContext.Should().BeOfType<SearchContext>();
    }

    [Theory]
    [AutoDbData]
    public void Get_NoHttpContextQuery_ShouldReturnNull(Item item)
    {
      var context = new RenderingContext
                    {
                      Rendering = new Rendering
                                  {
                                    Item = item
                                  }
                    };
      ContextService.Get().Push(context);
      var repository = new SearchContextRepository();
      var searchContext = repository.Get();
      searchContext.Should().BeNull();
    }
  }
}