using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Foundation.MultiSite.Tests
{
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.Foundation.MultiSite.Providers;
  using Sitecore.Foundation.MultiSite.Tests.Extensions;
  using Xunit;

  public class ItemDatasourceProviderTests
  {
    [Theory]
    [AutoDbData]
    public void GetSources_ShouldReturnSourcesFromSettingItem([Frozen]ISettingsProvider settingsProvider, [Greedy]ItemDatasourceProvider provider, string name, Item contextItem, Db db, string settingItemName, Item item, DbItem sourceRoot)
    {
      provider.Database = db.Database;
      var settingId = ID.NewID;
      db.Add(new DbItem(settingItemName, settingId, Templates.DatasourceConfiguration.ID) {new DbField(Templates.DatasourceConfiguration.Fields.DatasourceLocation) { {"en", sourceRoot.ID.ToString()} } });
      db.Add(sourceRoot);
      var sourceRootItem = db.GetItem(sourceRoot.ID);
      var settingItem = db.GetItem(settingId);
      settingsProvider.GetSettingItem(Arg.Any<string>(), Arg.Any<Item>()).Returns(settingItem);
      var sources = provider.GetSources(name, item);
      sources.Should().NotBeNull();
      sources.Should().Contain(sourceRootItem);
    }

    [Theory]
    [AutoDbData]
    public void GetSourceTemplate_ShouldReturnTemplateFromSettingItem([Frozen]ISettingsProvider settingsProvider, [Greedy]ItemDatasourceProvider provider, string name, Item contextItem, Db db, string settingItemName, Item item, DbItem sourceTemplate)
    {
      provider.Database = db.Database;
      var settingId = ID.NewID;
      db.Add(new DbItem(settingItemName, settingId) { new DbField(Templates.DatasourceConfiguration.Fields.DatasourceTemplate) { { "en", sourceTemplate.ID.ToString() } } });
      db.Add(sourceTemplate);
      var sourceRootItem = db.GetItem(sourceTemplate.ID);
      var settingItem = db.GetItem(settingId);
      settingsProvider.GetSettingItem(Arg.Any<string>(), Arg.Any<Item>()).Returns(settingItem);
      var sources = provider.GetSourceTemplate(name, item);
      sources.Should().NotBeNull();
      sources.ID.ShouldBeEquivalentTo(sourceRootItem.ID);
    }

    [Theory]
    [AutoDbData]
    public void GetSourceTemplates_ShouldReturnNull(ISettingsProvider settingsProvider, ItemDatasourceProvider itemDsProvider, string settingName, Item contextItem)
    {
      var provider = new ItemDatasourceProvider(settingsProvider);
      var template = provider.GetSourceTemplate(settingName, contextItem);
    }
  }
}
