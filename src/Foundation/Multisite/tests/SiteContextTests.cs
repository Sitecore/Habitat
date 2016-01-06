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
    public void GetSiteDefinition_ItemInSiteHierarcy_ShouldReturnHierarchicalSiteDefinition(SiteContext siteContext, DbItem item , Db db, string siteName)
    {
      var siteDefinitionId = ID.NewID;
      db.Add(new DbItem(siteName, siteDefinitionId, Templates.Site.ID) {item});
      var contextItem = db.GetItem(item.ID);
      var definitionItem = db.GetItem(siteDefinitionId);
      var siteDefinition = siteContext.GetSiteDefinition(contextItem);
      siteDefinition.Item.ID.ShouldBeEquivalentTo(definitionItem.ID);
    }

    [Theory]
    [AutoDbData]
    public void GetSiteDefinition_ItemOutsideSiteHierarcy_ShouldReturnContextSiteDefinition(SiteContext siteContext, DbItem item, Db db, string siteName)
    {
      var siteDefinitionId = ID.NewID;
      var home = new DbItem("home", ID.NewID);
      db.Add(new DbItem(siteName, siteDefinitionId, Templates.Site.ID) { home });
      var definitionItem = db.GetItem(siteDefinitionId);

      db.Add(item);
      var contextItem = db.GetItem(item.ID);

      var fakeSite = new Sitecore.FakeDb.Sites.FakeSiteContext(new Sitecore.Collections.StringDictionary { { "name", siteName }, { "database", db.Database.Name }, { "rootPath", "/sitecore/content/" + siteName }, { "startItem", "/home" } });
      using (new Sitecore.Sites.SiteContextSwitcher(fakeSite))
      {
        var siteDefinition = siteContext.GetSiteDefinition(contextItem);
        siteDefinition.Item.ID.ShouldBeEquivalentTo(definitionItem.ID);
      }
    }
  }
}
