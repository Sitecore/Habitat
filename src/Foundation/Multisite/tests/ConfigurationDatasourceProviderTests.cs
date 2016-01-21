namespace Sitecore.Foundation.Multisite.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Foundation.Multisite.Tests.Extensions;
  using Sitecore.Web;
  using Xunit;

  public class ConfigurationDatasourceProviderTests
  {
    [Theory]
    [AutoDbData]
    public void GetDataSources_ShouldReturnDatasourceSettingsFromSiteDefinition([Frozen]ISettingsProvider settingsProvider, [Greedy]ConfigurationDatasourceProvider provider, string settingName, Item contextItem, ID sourceLocationId, DbItem sourceDbItem, Db db)
    {
      provider.Database = db.Database;
      db.Add(sourceDbItem);
      var sourceItem = db.GetItem(sourceDbItem.ID);
      var attributeName = $"{settingName}.{ConfigurationDatasourceProvider.DatasourceLocationPostfix}";
      var siteInfo = new SiteInfo(new StringDictionary { {attributeName, sourceItem.ID.ToString()} });
      settingsProvider.GetCurrentSiteInfo(Arg.Any<Item>()).Returns(siteInfo);
      var sources = provider.GetDatasources(settingName, contextItem);
      sources.Should().NotBeNull();
      sources.Should().Contain(sourceItem);
    }

    [Theory]
    [AutoDbData]
    public void GetDataSources_ShouldReturnSourceTemplateFromSiteDefinition([Frozen]ISettingsProvider settingsProvider, [Greedy]ConfigurationDatasourceProvider provider, string settingName, Item contextItem, DbItem sourceDbItem, Db db)
    {
      provider.Database = db.Database;
      db.Add(sourceDbItem);
      var sourceTemplate = db.GetItem(sourceDbItem.ID);
      var attributeName = $"{settingName}.{ConfigurationDatasourceProvider.DatasourceTemplatePostfix}";
      var siteInfo = new SiteInfo(new StringDictionary { { attributeName, sourceTemplate.ID.ToString() } });
      settingsProvider.GetCurrentSiteInfo(Arg.Any<Item>()).Returns(siteInfo);
      var sources = provider.GetDatasourceTemplate(settingName, contextItem);
      sources.Should().NotBeNull();
      sources.ID.ShouldBeEquivalentTo(sourceTemplate.ID);
    }

    [Theory]
    [AutoDbData]
    public void Constructor_EmptyParameters_ShouldCreateInstance(ConfigurationDatasourceProvider provider)
    {
      provider.Should().NotBeNull();
    }

    [Theory]
    [AutoDbData]
    public void GetDataSourceTemplate_SiteInfoIsNull_ShouldReturnNull([Frozen]ISettingsProvider settingsProvider, [Greedy]ConfigurationDatasourceProvider provider, string settingName, Item contextItem, DbItem sourceDbItem, Db db)
    {
      provider.Database = db.Database;
      db.Add(sourceDbItem);
      settingsProvider.GetCurrentSiteInfo(Arg.Any<Item>()).Returns((SiteInfo)null);
      var sources = provider.GetDatasourceTemplate(settingName, contextItem);
      sources.Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void GetDataSources_SiteInfoIsNull_ShouldReturnNull([Frozen]ISettingsProvider settingsProvider, [Greedy]ConfigurationDatasourceProvider provider, string settingName, Item contextItem, DbItem sourceDbItem, Db db)
    {
      provider.Database = db.Database;
      db.Add(sourceDbItem);
      settingsProvider.GetCurrentSiteInfo(Arg.Any<Item>()).Returns((SiteInfo)null);
      var sources = provider.GetDatasources(settingName, contextItem);
      sources.Should().HaveCount(0);
    }

    [Theory]
    [AutoDbData]
    public void GetDataSources_SourceLocationSettingNotSet_ShouldReturnNull([Frozen]ISettingsProvider settingsProvider, [Greedy]ConfigurationDatasourceProvider provider, string settingName, Item contextItem, DbItem sourceDbItem, Db db)
    {
      provider.Database = db.Database;
      db.Add(sourceDbItem);
      var siteInfo = new SiteInfo(new StringDictionary());
      settingsProvider.GetCurrentSiteInfo(Arg.Any<Item>()).Returns(siteInfo);
      var sources = provider.GetDatasources(settingName, contextItem);
      sources.Should().HaveCount(0);
    }

    [Theory]
    [AutoDbData]
    public void GetDataSources_SourceLocationItemNotPresentInDb_ShouldReturnNull([Frozen]ISettingsProvider settingsProvider, [Greedy]ConfigurationDatasourceProvider provider, string settingName, Item contextItem, DbItem sourceDbItem, Db db)
    {
      provider.Database = db.Database;
      db.Add(sourceDbItem);
      var sourceItem = db.GetItem(sourceDbItem.ID);
      var attributeName = $"{settingName}.sourceLocation";
      var siteInfo = new SiteInfo(new StringDictionary { { attributeName, ID.NewID.ToString() } });
      settingsProvider.GetCurrentSiteInfo(Arg.Any<Item>()).Returns(siteInfo);
      var sources = provider.GetDatasources(settingName, contextItem);
      sources.Should().HaveCount(0);
    }

    [Theory]
    [AutoDbData]
    public void GetDataSourceTemplate_SourceTemplateSettinIsNotSet_ShouldReturnNull([Frozen]ISettingsProvider settingsProvider, [Greedy]ConfigurationDatasourceProvider provider, string settingName, Item contextItem, DbItem sourceDbItem, Db db)
    {
      provider.Database = db.Database;
      db.Add(sourceDbItem);
      var siteInfo = new SiteInfo(new StringDictionary());
      settingsProvider.GetCurrentSiteInfo(Arg.Any<Item>()).Returns(siteInfo);
      var template = provider.GetDatasourceTemplate(settingName, contextItem);
      template.Should().BeNull();
    }
  }
}
