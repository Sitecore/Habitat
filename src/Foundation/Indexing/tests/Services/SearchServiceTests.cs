using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Foundation.Indexing.Tests.Services
{
  using FluentAssertions;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.FakeDb;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.Indexing.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class SearchServiceTests
  {
    [Theory]
    [AutoDbData]
    public void ContextItem_NoSettingsAndNoContext_ShouldThrowException([Substitute, Frozen]ISearchSettings settings, SearchService searchService)
    {
      //Arrange
      //Act
      Action a = () => { var item = searchService.ContextItem; };
      a.ShouldThrow<Exception>();
      //Assert      
    }

    [Theory]
    [AutoDbData]
    public void ContextItem_HasSettingsRoot_ShouldReturnSettingsRoot(Db db, DbItem rootItem, [Substitute]ISearchSettings settings)
    {
      db.Add(rootItem);
      //Arrange
      settings.Root = db.GetItem(rootItem.ID);
      var searchService = new SearchService(settings);
      //Act
      var item = searchService.ContextItem;
      //Assert      
      item.Item.ID.ShouldBeEquivalentTo(rootItem.ID);
    }
  }
}
