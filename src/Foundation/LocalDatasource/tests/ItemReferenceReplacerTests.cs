namespace Sitecore.Foundation.LocalDatasource.Tests
{
  using FluentAssertions;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.LocalDatasource.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class ItemReferenceReplacerTests
  {
    [Theory]
    [AutoDbData]
    public void ReplaceItemReferences_ItemPassed_ShouldReplaceShortID(ItemReferenceReplacer referenceReplacer, Db db, [Content] Item source, [Content] Item target)
    {
      var initialValue = source.ID.ToShortID().ToString();
      var expectedValue = target.ID.ToShortID().ToString();

      ReplaceItemReferences_ItemPassed_ShouldReplaceValue(referenceReplacer, db, source, target, initialValue, expectedValue);
    }


    [Theory]
    [AutoDbData]
    public void ReplaceItemReferences_ItemPassed_ShouldReplaceID(ItemReferenceReplacer referenceReplacer, Db db, [Content] Item source, [Content] Item target)
    {
      var initialValue = source.ID.ToString();
      var expectedValue = target.ID.ToString();

      ReplaceItemReferences_ItemPassed_ShouldReplaceValue(referenceReplacer, db, source, target, initialValue, expectedValue);
    }


    [Theory]
    [AutoDbData]
    public void ReplaceItemReferences_ItemPassed_ShouldReplaceFullPath(ItemReferenceReplacer referenceReplacer, Db db, [Content] Item source, [Content] Item target)
    {
      var initialValue = source.Paths.FullPath;
      var expectedValue = target.Paths.FullPath;


      ReplaceItemReferences_ItemPassed_ShouldReplaceValue(referenceReplacer, db, source, target, initialValue, expectedValue);
    }


    [Theory]
    [AutoDbData]
    public void ReplaceItemReferences_ItemPassed_ShouldReplaceContentPath(ItemReferenceReplacer referenceReplacer, Db db, [Content] Item source, [Content] Item target)
    {
      var initialValue = source.Paths.ContentPath;
      var expectedValue = target.Paths.ContentPath;

      ReplaceItemReferences_ItemPassed_ShouldReplaceValue(referenceReplacer, db, source, target, initialValue, expectedValue);
    }


    private static void ReplaceItemReferences_ItemPassed_ShouldReplaceValue(ItemReferenceReplacer referenceReplacer, Db db, Item source, Item target, string initialValue, string expectedValue)
    {
      referenceReplacer.AddItemPair(source, target);

      db.Add(new DbItem("testItem")
      {
        {
          "targetField", initialValue
        }
      });
      var item = db.GetItem("/sitecore/content/testItem");
      referenceReplacer.ReplaceItemReferences(item);
      item["targetField"].Should().Be(expectedValue);
    }
  }
}