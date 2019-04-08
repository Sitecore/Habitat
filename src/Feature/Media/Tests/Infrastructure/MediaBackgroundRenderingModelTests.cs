namespace Sitecore.Feature.Media.Tests.Infrastructure
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.UI.WebControls;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.Feature.Media.Infrastructure.Models;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Resources.Media;
  using Xunit;

  public class MediaBackgroundRenderingModelTests
  {
    [Theory]
    [AutoDbData]
    public void MediaType_ListOfTypes_ReturnFirstMediType(Db db, List<string> mimeTypes, MediaBackgroundRenderingModel model)
    {
      //Arrange
      var id = ID.NewID;
      db.Add(new DbItem("media", id)
      {
        {"Mime type", string.Join("/", mimeTypes) }
      });

      model.Media = db.GetItem(id).Paths.FullPath;

      //Act
      model.MediaType.Should().Be(mimeTypes.First());
    }

    [Theory]
    [AutoDbData]
    public void MediaFormat_ListOfTypes_ReturnLastMimeType(Db db, List<string> mimeTypes, MediaBackgroundRenderingModel model)
    {
      //Arrange
      var id = ID.NewID;
      db.Add(new DbItem("media", id)
      {
        {"Mime type", string.Join("/", mimeTypes) }
      });

      model.Media = db.GetItem(id).Paths.FullPath;

      //Act
      model.MediaFormat.Should().Be(mimeTypes.Last());
    }

    [Theory]
    [AutoDbData]
    public void MediaType_EmptyMedia_ReturnEmpty(MediaBackgroundRenderingModel model)
    {
      //Arrange
      model.Media = null;
      //Act
      model.MediaType.Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void MediaFormat_EmptyMedia_ReturnEmpty(MediaBackgroundRenderingModel model)
    {
      //Arrange
      model.Media = null;
      //Act
      model.MediaFormat.Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void CssClass_NoParallax_ReturnTypeAsClass(string cssClass, MediaBackgroundRenderingModel model)
    {
      //Arrange
      model.Type = cssClass;
      model.Parallax = "false";
      //Act
      model.CssClass.Should().Be(cssClass);
    }

    [Theory]
    [AutoDbData]
    public void CssClass_Parallax_ReturnTypeAndParallaxClass(string cssClass, MediaBackgroundRenderingModel model)
    {
      //Arrange
      model.Type = cssClass;
      model.Parallax = "true";
      //Act
      model.CssClass.Should().Be($"{cssClass} bg-parallax");
    }

    [Theory]
    [AutoDbData]
    public void IsMedia_NullType_ReturnFalse(MediaBackgroundRenderingModel model)
    {
      //Arrange
      model.Type = null;
      //Act
      model.IsMedia.Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void IsMedia_WrongType_ReturnFalse(string type, MediaBackgroundRenderingModel model)
    {
      //Arrange
      model.Type = type;
      //Act
      model.IsMedia.Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void IsMedia_CorrectType_ReturnTrue(MediaBackgroundRenderingModel model)
    {
      //Arrange
      model.Type = "bg-media";
      //Act
      model.IsMedia.Should().BeTrue();
    }

    [Theory]
    [AutoDbData]
    public void IsMedia_NotSetMediaItem_EmptyBackgroundUrl(MediaBackgroundRenderingModel model)
    {
      //Arrange
      model.Type = "bg-media";
      model.Media = null;
      //Act
      model.MediaAttribute.Should().Be("style=background-image:url('');");
    }

    [Theory]
    [AutoDbData]
    public void IsMedia_MediaItemIsSet_EmptyBackgroundUrl(Db db, MediaBackgroundRenderingModel model)
    {
      //Arrange
      model.Type = "bg-media";
      var id = ID.NewID;
      db.Add(new DbItem("media", id));

      model.Media = db.GetItem(id).Paths.FullPath;
      var mediaUrl = MediaManager.GetMediaUrl(db.GetItem(id));

      //Act
      model.MediaAttribute.Should().Be($"style=background-image:url('{mediaUrl}');");
    }


  }
}
