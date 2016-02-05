namespace Sitecore.Foundation.SitecoreExtensions.Tests.Repositories
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.SitecoreExtensions.Model;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class FileRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Get_ValidXml_ShouldReturnImage()
    {
      FileRepository.Get("<image/>").Should().NotBeNull();
    }
    [Theory]
    [AutoDbData]
    public void Get_AttributesSet_ShouldReturnImage()
    {
      var file = FileRepository.Get("<image src='mSrc' mediaid='mId' />");
      file.Should().NotBeNull();
      file.MediaId.Should().Be("mId");
      file.Source.Should().Be("mSrc");

    }

    [Theory]
    [AutoDbData]
    public void Get_MediaIdRefersExistingItem_ShouldImage([Content]Item item)
    {
      var image = FileRepository.Get($"<image src='mSrc' mediaid='{item.ID}' />");
      image.Should().NotBeNull();
      image.MediaId.Should().Be(item.ID.ToString());
      image.Title.Should().Be(item.DisplayName);

    }
  }
}