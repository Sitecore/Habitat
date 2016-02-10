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

  public class ImageRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Get_ValidXml_ShouldReturnImage()
    {
      ImageRepository.Get("<image/>").Should().NotBeNull();
    }
    [Theory]
    [AutoDbData]
    public void Get_AttributesSet_ShouldReturnImage()
    {
      var image = ImageRepository.Get("<image src='mSrc' mediapath='mPath' alt='mAlt' width='1' height='2' vspace='3'  hspace='4'/>");
      image.Should().NotBeNull();
      image.AlternateText.Should().Be("mAlt");
      
      image.MediaPath.Should().Be("mPath");
      image.Width.Should().Be(1);
      image.Height.Should().Be(2);
      image.VerticalSpace.Should().Be(3);
      image.HorisontalSpace.Should().Be(4);
    }

    [Theory]
    [AutoDbData]
    public void Get_MediaIdRefersExistingItem_ShouldReturnImage([Content]Item item)
    {
      var image = ImageRepository.Get($"<image src='mSrc' mediaid='{item.ID}' />");
      image.Should().NotBeNull();
      image.MediaId.Should().Be(item.ID.ToString());
      image.Title.Should().Be(item.DisplayName);
      
    }
  }
}