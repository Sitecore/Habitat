namespace Sitecore.Foundation.MultiSite.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.Foundation.MultiSite.Tests.Extensions;
  using Xunit;

  public class SiteContextTests
  {
    [Theory]
    [AutoDbData]
    public void GetSiteDefinitionByItem_ShouldReturnCurrentSiteDefinition(SiteContext siteContext, DbItem item , Db db, string siteName)
    {
      var definitionId = ID.NewID;
      db.Add(new DbItem(siteName, definitionId, Templates.Site.ID) {item});
      var contextItem = db.GetItem(item.ID);
      var definitionItem = db.GetItem(definitionId);
      var siteDefinition = siteContext.GetSiteDefinitionByItem(contextItem);
      siteDefinition.Item.ID.ShouldBeEquivalentTo(definitionItem.ID);
    }
  }
}
