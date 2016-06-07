namespace Sitecore.Foundation.Datasource.Tests
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.LocalDatasource.Extensions;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Links;
  using Xunit;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.LocalDatasource.Services;

  public class UpdateLocalDatasourceReferencesServiceTests
  {
    [Theory]
    [AutoDbData]
    public void Update_ItemPassed_ShouldReplaceLinks([Substitute] LinkDatabase linkDb, Db db)
    {
      var datasourceItemId = ID.NewID;

      db.Add(new DbItem("source") {Children = { new DbItem("_Local") { new DbItem("DatasourceItem") }}, Fields = { "testField"}});;
      db.Add(new DbItem("target") {Children = { new DbItem("_Local") { new DbItem("DatasourceItem") }}, Fields = { "testField"}});;
      
      

     
      var sourceItem = db.GetItem("/sitecore/content/source");
      var targetItem = db.GetItem("/sitecore/content/target");
      var datasourceItem = db.GetItem("/sitecore/content/source/_Local/DatasourceItem");
      var targetDatasourceItem = db.GetItem("/sitecore/content/target/_Local/DatasourceItem");
      var itemLinks = new[]
      {
        new ItemLink(sourceItem, FieldIDs.LayoutField, datasourceItem, string.Empty)
      };

      linkDb.GetReferences(sourceItem).Returns(itemLinks.ToArray());
      using (new Sitecore.FakeDb.Links.LinkDatabaseSwitcher(linkDb))
      {
        using (new EditContext(targetItem))
        {
          targetItem["__Renderings"] = datasourceItem.ID.ToString();
        }
        var referenceReplacer = new UpdateLocalDatasourceReferencesService(sourceItem, targetItem);

        referenceReplacer.Update();

        var expectedValue = targetDatasourceItem.ID.ToString();
        targetItem["__Renderings"].Should().Be(expectedValue);
      }
    }

    
  }
}