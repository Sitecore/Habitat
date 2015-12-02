namespace Sitecore.Framework.SitecoreExtensions.Tests.Extensions
{
  using System;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Framework.SitecoreExtensions.Extensions;
  using Sitecore.Framework.SitecoreExtensions.Tests.Common;
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
      ItemExtensions.MediaUrl(mediaItem, template.FieldId).Should().NotBeNull();
      ItemExtensions.MediaUrl(mediaItem, template.FieldId).Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void MediaUrlShoulReturnLink([Content] Db db, [Content] Item target, [Content] MediaTemplate template, string expectedUri)
    {
      var newID = ID.NewID;
      db.Add(new Sitecore.FakeDb.DbItem("home", newID, template.ID)
      {
        new Sitecore.FakeDb.DbLinkField("medialink", template.FieldId)
        {
          LinkType = "media",
          TargetID = target.ID

        }
      });

      Sitecore.Resources.Media.MediaProvider mediaProvider =
        NSubstitute.Substitute.For<Sitecore.Resources.Media.MediaProvider>();

      mediaProvider
        .GetMediaUrl(Arg.Is<Sitecore.Data.Items.MediaItem>(i => i.ID == target.ID))
        .Returns(expectedUri);

      // substitute the original provider with the mocked one
      using (new Sitecore.FakeDb.Resources.Media.MediaProviderSwitcher(mediaProvider))
        ItemExtensions.MediaUrl(Database.GetDatabase("master").GetItem(newID), template.FieldId).Should().Be(expectedUri);
    }

    [Theory]
    [AutoDbData]
    public void MediaUrlShouldReturnEmptyStringWhenLinkIsBroken([Content] Db db, [Content] Item target, [Content] MediaTemplate template, string expectedUri)
    {
      var newID = ID.NewID;
      db.Add(new Sitecore.FakeDb.DbItem("home", newID, template.ID)
      {
        new Sitecore.FakeDb.DbLinkField("medialink", template.FieldId)
        {
          LinkType = "media",

        }
      });

      Sitecore.Resources.Media.MediaProvider mediaProvider =
        NSubstitute.Substitute.For<Sitecore.Resources.Media.MediaProvider>();

      mediaProvider
        .GetMediaUrl(Arg.Is<Sitecore.Data.Items.MediaItem>(i => i.ID == target.ID))
        .Returns(expectedUri);

      // substitute the original provider with the mocked one
      using (new Sitecore.FakeDb.Resources.Media.MediaProviderSwitcher(mediaProvider))
        ItemExtensions.MediaUrl(Database.GetDatabase("master").GetItem(newID), template.FieldId).Should().Be("");
    }
  }
}