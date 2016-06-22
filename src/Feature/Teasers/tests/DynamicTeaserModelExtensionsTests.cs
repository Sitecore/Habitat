namespace Sitecore.Feature.Teasers.Tests
{
  using FluentAssertions;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Teasers.Extensions;
  using Sitecore.Feature.Teasers.Models;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class DynamicTeaserModelExtensionsTests
  {
    [Theory]
    [AutoDbData]
    public void GetMaxImageHeight_EmptyInnerItems_ZeroHeight(DynamicTeaserModel model)
    {
      //Act
      var maxHeight = model.GetMaxImageHeight();
      //Assert      
      maxHeight.Should().Be(0);
    }

    [Theory]
    [AutoDbData]
    public void GetMaxImageHeight_NoImageItems_ZeroHeight(Db db,DbItem modelItem)
    {
      //Arrange
      modelItem.Add(new DbItem("Inner item 1",new ID(), Templates.TeaserHeadline.ID));
      modelItem.Add(new DbItem("Inner item 2", new ID(), Templates.TeaserHeadline.ID));
      db.Add(modelItem);

      var model = new DynamicTeaserModel(db.GetItem(modelItem.ID));
      //Act
      var maxHeight = model.GetMaxImageHeight();
      //Assert      
      maxHeight.Should().Be(0);
    }

    [Theory]
    [AutoDbData]
    public void GetMaxImageHeight_ImageItems_ReturnMaxHeight(Db db, int maxHeight, int difference, DbItem modelItem)
    {
      //Arrange
      modelItem.Add(new DbItem("Inner item 1", new ID(), Templates.TeaserHeadline.ID) { {Templates.TeaserContent.Fields.Image, $@"<image mediapath="""" src="""" height=""{maxHeight}"" mediaid="""" ></image>" } });
      modelItem.Add(new DbItem("Inner item 2", new ID(), Templates.TeaserHeadline.ID) { { Templates.TeaserContent.Fields.Image, $@"<image mediapath="""" src="""" height=""{maxHeight - difference}"" mediaid="""" ></image>" } });
      db.Add(modelItem);

      var model = new DynamicTeaserModel(db.GetItem(modelItem.ID));
      //Act
      var result= model.GetMaxImageHeight();
      //Assert      
      result.Should().Be(maxHeight);
    }
  }
}
