namespace Sitecore.Foundation.Multisite.Tets
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Foundation.Multisite.Tets.Extensions;
  using Sitecore.Shell.Framework.Commands.Preferences;
  using Sitecore.Sites;
  using Xunit;

  public class ItemSiteDefinitionProviderTests
  {
    [Theory]
    [AutoDbData]
    public void SiteDefinitions_ShouldReturnSiteDefinitionFromTheItemList(Db db, ItemSiteDefinitionsProvider provider, SiteDefinitionsProviderBase baseProvider)
    {
      db.Add(new DbItem("site1", ID.NewID, Templates.Site.ID));
      db.Add(new DbItem("site2", ID.NewID, Templates.Site.ID));
      db.Add(new DbItem("site3", ID.NewID, Sitecore.TemplateIDs.StandardTemplate));
      var fakeSite = new FakeSiteContext(new StringDictionary
      {
        {"displayMode", "normal"},
        {"rootPath", "/sitecore/content"},
        { "name", "site1"}
      }) as SiteContext;

      using (new SiteContextSwitcher(fakeSite))
      {
        var results = provider.SiteDefinitions;
        results.Count().ShouldBeEquivalentTo(2);
        results.Should().As<IEnumerable<SiteDefinitionItem>>();
      }
    }

    [Theory]
    [AutoDbData]
    public void SiteDefinitions_ContextDoesNotExists_ShouldReturnNull(Db db, ItemSiteDefinitionsProvider provider)
    {
      db.GetItem(Sitecore.ItemIDs.ContentRoot).Delete();
      provider.SiteDefinitions.Should().BeNull();
    }
  }
}
