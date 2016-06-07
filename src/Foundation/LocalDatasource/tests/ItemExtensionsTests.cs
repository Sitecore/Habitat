namespace Sitecore.Foundation.Datasource.Tests
{
  using FluentAssertions;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.LocalDatasource.Extensions;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class ItemExtensionsTests
  {
    [Theory]
    [AutoDbData]
    public void HasLocalDatasourceFolder_ItemWithoutLocalDatasource_ReturnsFalse([Content] Item item)
    {
      item.HasLocalDatasourceFolder().Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void HasLocalDatasourceFolder_ItemWithLocalDatasource_ReturnsFalse([Content] DbItem item, Db db)
    {
      item.Add(new DbItem("_Local"));
      db.GetItem(item.ID).HasLocalDatasourceFolder().Should().BeTrue();
    }
  }
}