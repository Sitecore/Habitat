namespace Sitecore.Foundation.SitecoreExtensions.Tests.Extensions
{
  using System;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Resources.Media;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Resources.Media;
  using Xunit;

  public class ItemExtensionsTests
  {
    [Theory]
    [AutoDbData]
    public void MediaUrlShouldThrowExceptionWhenItemNull()
    {
      Action action = () => ItemExtensions.MediaUrl(null, ID.NewID);
      action.ShouldThrow<ArgumentNullException>();
    }


    [Theory]
    [AutoDbData]
    public void MediaUrlShoulReturnEmptyStringWhenLinkNull([Content] Item item, [Content] MediaTemplate template)
    {
      var mediaItem = item.Add("media", new TemplateID(template.ID));
      mediaItem.MediaUrl(template.FieldId).Should().NotBeNull();
      mediaItem.MediaUrl(template.FieldId).Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void MediaUrlShoulReturnLink([Content] Db db, [Content] Item target, [Content] MediaTemplate template, string expectedUri)
    {
      var newID = ID.NewID;
      db.Add(new DbItem("home", newID, template.ID)
      {
        new DbLinkField("medialink", template.FieldId)
        {
          LinkType = "media",
          TargetID = target.ID
        }
      });

      var mediaProvider =
        Substitute.For<MediaProvider>();

      mediaProvider
        .GetMediaUrl(Arg.Is<MediaItem>(i => i.ID == target.ID))
        .Returns(expectedUri);

      // substitute the original provider with the mocked one
      using (new MediaProviderSwitcher(mediaProvider))
      {
        Database.GetDatabase("master").GetItem(newID).MediaUrl(template.FieldId).Should().Be(expectedUri);
      }
    }

    [Theory]
    [AutoDbData]
    public void MediaUrlShouldReturnEmptyStringWhenLinkIsBroken([Content] Db db, [Content] Item target, [Content] MediaTemplate template, string expectedUri)
    {
      var newID = ID.NewID;
      db.Add(new DbItem("home", newID, template.ID)
      {
        new DbLinkField("medialink", template.FieldId)
        {
          LinkType = "media"
        }
      });

      var mediaProvider =
        Substitute.For<MediaProvider>();

      mediaProvider
        .GetMediaUrl(Arg.Is<MediaItem>(i => i.ID == target.ID))
        .Returns(expectedUri);

      // substitute the original provider with the mocked one
      using (new MediaProviderSwitcher(mediaProvider))
      {
        Database.GetDatabase("master").GetItem(newID).MediaUrl(template.FieldId).Should().Be("");
      }
    }

    [Theory]
    [AutoDbData]
    public void HasFieldValue_FieldHasValue_ShouldReturnTrue(Db db, DbItem item, DbField field, string value)
    {
      //Arrange
      field.Value = value;
      item.Add(field);
      db.Add(item);
      var testItem = db.GetItem(item.ID);
      //Act
      testItem.FieldHasValue(field.ID).Should().BeTrue();
    }

    [Theory]
    [AutoDbData]
    public void HasFieldValue_FieldIsNull_ShouldReturnFalse(Db db, DbItem item, DbField field)
    {
      //Arrange
      field.Value = null;
      item.Add(field);
      db.Add(item);
      var testItem = db.GetItem(item.ID);
      //Act
      testItem.FieldHasValue(field.ID).Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void HasFieldValue_FieldDoesNotExist_ShouldReturnFalse(Db db, DbItem item, DbField field)
    {
      //Arrange
      db.Add(item);
      var testItem = db.GetItem(item.ID);
      //Act
      testItem.FieldHasValue(field.ID).Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void HasFieldValue_FieldHasStandardValue_ShouldReturnTrue(Db db, string itemName, TemplateID templateId, ID fieldId, string value)
    {
      var template = new DbTemplate("Sample", templateId)
                     {
                       {fieldId, value}
                     };
      db.Add(template);
      //Arrange
      var contentRoot = db.GetItem(ItemIDs.ContentRoot);
      var item = contentRoot.Add("Home", new TemplateID(template.ID));

      //Act
      item.FieldHasValue(fieldId).Should().BeTrue();
    }
  }
}