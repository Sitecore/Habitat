﻿namespace Sitecore.Foundation.SitecoreExtensions.Tests.Extensions
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
  using Sitecore.Resources.Media;
  using UnitTests.Common.Attributes;
  using Xunit;

  public class ItemExtensionsTests
  {
    [Theory]
    [AutoDbData]
    public void DisplayNameShouldReturnActualValue([Content] Item item)
    {
      item.ID.DisplayName().Should().BeEquivalentTo(item.DisplayName);
      item.ID.Guid.DisplayName().Should().BeEquivalentTo(item.DisplayName);
    }


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
  }
}