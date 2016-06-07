namespace Sitecore.Foundation.LocalDatasource.Tests
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Links;
  using Sitecore.Foundation.LocalDatasource.Extensions;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Links;
  using Xunit;

  public class ItemExtensionsTests
  {
    [Theory]
    [AutoDbData]
    public void HasLocalDatasourceFolder_ItemWithoutLocalDatasource_ShouldReturnFalse([Content] Item item)
    {
      item.HasLocalDatasourceFolder().Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void HasLocalDatasourceFolder_ItemWithLocalDatasource_ShouldReturnFalse([Content] Item item, Db db)
    {
      item.Add("_Local", new TemplateID(item.TemplateID));
      db.GetItem(item.ID).HasLocalDatasourceFolder().Should().BeTrue();
    }

    [Theory]
    [AutoDbData]
    public void HasLocalDatasourceFolder_NullPassed_ShouldThrowException()
    {
      ((Item)null).Invoking(x => x.HasLocalDatasourceFolder()).ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [AutoDbData]
    public void GetLocalDatasourceFolder_ItemWithoutLocalDatasource_ShouldReturnNull([Content] Item item)
    {
      item.GetLocalDatasourceFolder().Should().Be(null);
    }

    [Theory]
    [AutoDbData]
    public void GetLocalDatasourceFolder_ItemWithLocalDatasource_ShouldReturnNull([Content] Item item)
    {
      var expectedItem = item.Add("_Local", new TemplateID(item.TemplateID));

      item.GetLocalDatasourceFolder().Should().NotBeNull(null);
      item.GetLocalDatasourceFolder().ID.Should().Be(expectedItem.ID);
    }

    [Theory]
    [AutoDbData]
    public void GetLocalDatasourceFolder_NullPassed_ShouldThrowException()
    {
      ((Item)null).Invoking(x => x.GetLocalDatasourceFolder()).ShouldThrow<ArgumentNullException>();
    }


    [Theory]
    [AutoDbData]
    public void IsLocalDatasourceItem_ItemWithoutLocalDatasource_ShouldReturnFalse([Content] Item item, [Content] Item ofItem)
    {
      item.IsLocalDatasourceItem(ofItem).Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void IsLocalDatasourceItem_WrongItem_ShouldReturnFalse([Content] Item item, [Content] Item ofItem)
    {
      var datasourceFolder = ofItem.Add("_Local", new TemplateID(ofItem.TemplateID));
      item.IsLocalDatasourceItem(ofItem).Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void IsLocalDatasourceItem_ItemWithLocalDatasource_ShouldReturnTrue([Content] Item ofItem)
    {
      var datasourceFolder = ofItem.Add("_Local", new TemplateID(ofItem.TemplateID));
      var datasourceItem = datasourceFolder.Add("DatasourceItem", new TemplateID(ofItem.TemplateID));

      datasourceItem.IsLocalDatasourceItem(ofItem).Should().BeTrue();
    }

    [Theory]
    [AutoDbData]
    public void IsLocalDatasourceItem_NullPassed_ShouldThrowException(Item notNullItem)
    {
      ((Item)null).Invoking(x => x.IsLocalDatasourceItem(notNullItem)).ShouldThrow<ArgumentNullException>();
    }


    [Theory]
    [AutoDbData]
    public void IsLocalDatasourceItem2_NullItem_ShouldThrowException()
    {
      ((Item)null).Invoking(x => x.IsLocalDatasourceItem()).ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [AutoDbData]
    public void IsLocalDatasourceItem_CorrectItemWhenTemplateIdAsSetting_ShouldReturnTrue([Content] Item item, [Content] DbTemplate template)
    {
      var datasourceFolder = item.Add("_Local", new TemplateID(template.ID));
      var datasourceItem = datasourceFolder.Add("DatasourceItem", new TemplateID(item.TemplateID));
      using (new SettingsSwitcher("Foundation.LocalDatasource.LocalDatasourceFolderTemplate", template.ID.ToString()))
      {
        datasourceItem.IsLocalDatasourceItem().Should().BeTrue();
      }
    }

    [Theory]
    [AutoDbData]
    public void IsLocalDatasourceItem_CorrectItemWhenTemplateNameAsSetting_ShouldReturnTrue([Content] Item item, [Content] DbTemplate template)
    {
      var datasourceFolder = item.Add("_Local", new TemplateID(template.ID));
      var datasourceItem = datasourceFolder.Add("DatasourceItem", new TemplateID(item.TemplateID));
      using (new SettingsSwitcher("Foundation.LocalDatasource.LocalDatasourceFolderTemplate", template.Name))
      {
        datasourceItem.IsLocalDatasourceItem().Should().BeTrue();
      }
    }

    [Theory]
    [AutoDbData]
    public void IsLocalDatasourceItem_WrongItemWhenTemplateNameAsSetting_ShouldReturnFalse([Content] Item item, [Content] DbTemplate template)
    {
      var datasourceFolder = item.Add("_Local", new TemplateID(item.TemplateID));
      var datasourceItem = datasourceFolder.Add("DatasourceItem", new TemplateID(item.TemplateID));
      using (new SettingsSwitcher("Foundation.LocalDatasource.LocalDatasourceFolderTemplate", template.Name))
      {
        datasourceItem.IsLocalDatasourceItem().Should().BeFalse();
      }
    }

    [Theory]
    [AutoDbData]
    public void IsLocalDatasourceItem_WrongItemWhenTemplateIdAsSetting_ShouldReturnFalse([Content] Item item, [Content] DbTemplate template)
    {
      var datasourceFolder = item.Add("_Local", new TemplateID(item.TemplateID));
      var datasourceItem = datasourceFolder.Add("DatasourceItem", new TemplateID(item.TemplateID));
      using (new SettingsSwitcher("Foundation.LocalDatasource.LocalDatasourceFolderTemplate", template.ID.ToString()))
      {
        datasourceItem.IsLocalDatasourceItem().Should().BeFalse();
      }
    }


    [Theory]
    [AutoDbData]
    public void GetParentLocalDatasourceFolder_NullPassed_ShouldThrowException()
    {
      ((Item)null).Invoking(x => x.GetParentLocalDatasourceFolder()).ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [AutoDbData]
    public void GetParentLocalDatasourceFolder_DatasourceTemplateIsNotSet_ShouldReturnNull([Content] Item item)
    {
      using (new SettingsSwitcher("Foundation.LocalDatasource.LocalDatasourceFolderTemplate", ID.NewID.ToString()))
      {
        item.GetParentLocalDatasourceFolder().Should().BeNull();
      }
    }


    [Theory]
    [AutoDbData]
    public void GetParentLocalDatasourceFolder_NoAncestorWithDatasourceTemplateIsSet_ShouldThrowException([Content] Item item, [Content] DbTemplate template)
    {
      var datasourceFolder = item.Add("_Local", new TemplateID(item.TemplateID));
      var datasourceItem = datasourceFolder.Add("DatasourceItem", new TemplateID(item.TemplateID));
      using (new SettingsSwitcher("Foundation.LocalDatasource.LocalDatasourceFolderTemplate", template.ID.ToString()))
      {
        datasourceItem.GetParentLocalDatasourceFolder().Should().BeNull();
      }
    }

    [Theory]
    [AutoDbData]
    public void GetParentLocalDatasourceFolder_HasAncestorWithDatasourceTemplateIsSet_ShouldThrowException([Content] Item item, [Content] DbTemplate template)
    {
      var datasourceFolder = item.Add("_Local", new TemplateID(template.ID));
      var datasourceItem = datasourceFolder.Add("DatasourceItem", new TemplateID(item.TemplateID));
      using (new SettingsSwitcher("Foundation.LocalDatasource.LocalDatasourceFolderTemplate", template.ID.ToString()))
      {
        datasourceItem.GetParentLocalDatasourceFolder().ID.Should().Be(datasourceFolder.ID);
      }
    }


    [Theory]
    [AutoDbData]
    public void GetLocalDatasourceDependencies_NoDatasourceFolder_ShouldReturnEmpty([Content] Item item, [Content] DbTemplate template)
    {
      item.GetLocalDatasourceDependencies().Length.Should().Be(0);
    }

    [Theory]
    [AutoDbData]
    public void GetLocalDatasourceDependencies_HasDatasourceFolder_ShouldReturnLinkedItem([Substitute] LinkDatabase linkDb, [Content] Item item, [Content] Item[] refItems, [Content] DbTemplate template)
    {
      var itemLinks = refItems.Select(x => new ItemLink(item, FieldIDs.LayoutField, x, string.Empty)).ToList();
      var datasourceFolder = item.Add("_Local", new TemplateID(template.ID));
      var datasourceItem = datasourceFolder.Add("DatasourceItem", new TemplateID(item.TemplateID));
      itemLinks.Add(new ItemLink(item, FieldIDs.LayoutField, datasourceItem, string.Empty));
      linkDb.GetReferences(item).Returns(itemLinks.ToArray());
      using (new LinkDatabaseSwitcher(linkDb))
      {
        var linkItem = item.GetLocalDatasourceDependencies().Single();
        linkItem.ID.Should().Be(datasourceItem.ID);
      }
    }
  }
}