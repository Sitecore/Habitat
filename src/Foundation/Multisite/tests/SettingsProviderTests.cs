﻿

namespace Sitecore.Foundation.MultiSite.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
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

  public class SettingsProviderTests
  {
    [Theory]
    [AutoDbData]
    public void GetSettingsItem_ShouldReturnSettingItem(string settingName, [Frozen]Item contextItem, [Substitute]SiteContext context, Db db, string definitionItemName)
    {
      var provider = new SettingsProvider(context);
      var settingItemId = ID.NewID;
      var definitionId = ID.NewID;
      db.Add(new DbItem(definitionItemName, definitionId) {new DbItem("Settings") {new DbItem(settingName, settingItemId, Templates.DatasourceConfiguration.ID)} });
      var definitionItem = db.GetItem(definitionId);
      var setting = db.GetItem(settingItemId);
      context.GetSiteDefinitionByItem(Arg.Any<Item>()).Returns(new SiteDefinitionItem {Item = definitionItem });
      var settingItem = provider.GetSettingItem(settingName, contextItem);
      settingItem.ID.ShouldBeEquivalentTo(setting.ID);
    }

    [Theory]
    [AutoDbData]
    public void GetSettingsItem_SiteDefinitionDoesNotExists_ShouldReturnNull(string settingName, [Frozen]Item contextItem, [Substitute]SiteContext context, Db db, string definitionItemName)
    {
      var provider = new SettingsProvider(context);
      context.GetSiteDefinitionByItem(Arg.Any<Item>()).Returns((SiteDefinitionItem)null);
      var settingItem = provider.GetSettingItem(settingName, contextItem);
      settingItem.Should().BeNull();
    }
  }
}
