namespace Sitecore.Feature.Media.Tests.Extensions
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.Feature.Media.Infrastructure;
  using UnitTests.Common.Attributes;
  using Xunit;

  public class RenderParallaxMediaTests
  {
    [Theory]
    [AutoDbData]
    public void RenderParallaxMediaAttributes_ForImage_ShouldReturnImageType(Db db)
    {
      var homeItemId = ID.NewID;
      var mediaItemId = ID.NewID;
      db.Add(new DbItem("home", homeItemId)
      {
        new DbLinkField("BackgroundMedia", Templates.HasParallaxBackground.Fields.BackgroundMedia)
        {
          LinkType = "media",
          TargetID = mediaItemId
        }
      });

      db.Add(new DbItem("mediaItem", mediaItemId)
      {
        {
          "Mime type", "image/fake"
        }
      });
      var homeItem = db.GetItem(homeItemId);

      var htmlString = homeItem.RenderParallaxMediaAttributes();
      var attributes = htmlString.ToString().Split(' ').Select(x => x.Split('=')).ToDictionary(x => x[0], val => val.Length == 1 ? "" : val[1].Trim('\'', '"'));

      attributes["data-multibackground-layer-0-attachment"].Should().Be("static");
      attributes["data-multibackground"].Should().BeEmpty();
      attributes["data-multibackground-layer-0-type"].Should().Be("image");
    }


    [Theory]
    [AutoDbData]
    public void RenderParallaxMediaAttributes_Video_ShouldReturnVideoType(Db db)
    {
      var homeItemId = ID.NewID;
      var mediaItemId = ID.NewID;
      db.Add(new DbItem("home", homeItemId)
      {
        new DbLinkField("BackgroundMedia", Templates.HasParallaxBackground.Fields.BackgroundMedia)
        {
          LinkType = "media",
          TargetID = mediaItemId
        }
      });

      db.Add(new DbItem("mediaItem", mediaItemId)
      {
        {
          "Mime type", "video/fake"
        }
      });
      var homeItem = db.GetItem(homeItemId);

      var htmlString = homeItem.RenderParallaxMediaAttributes();
      var attributes = htmlString.ToString().Split(' ').Select(x => x.Split('=')).ToDictionary(x => x[0], val => val.Length == 1 ? "" : val[1].Trim('\'', '"'));

      attributes["data-multibackground-layer-0-attachment"].Should().Be("static");
      attributes["data-multibackground"].Should().BeEmpty();
      attributes["data-multibackground-layer-0-type"].Should().Be("video");
      attributes["data-multibackground-layer-0-format"].Should().Be("fake");
    }

    [Theory]
    [AutoDbData]
    public void RenderParallaxMediaAttributes_ParallaxEnabled_ShouldReturnParallaxSpeed(Db db, string parallaxSpeed)
    {
      var homeItemId = ID.NewID;
      var mediaItemId = ID.NewID;
      db.Add(new DbItem("home", homeItemId)
      {
        new DbLinkField("BackgroundMedia", Templates.HasParallaxBackground.Fields.BackgroundMedia)
        {
          LinkType = "media",
          TargetID = mediaItemId
        },
        new DbField("ParalaxEnabled", Templates.HasParallaxBackground.Fields.IsParallaxEnabled)
        {
          Value = "1"
        },
        new DbField("ParalaxSpeed", Templates.HasParallaxBackground.Fields.ParallaxSpeed)
        {
          Value = parallaxSpeed
        }
      });

      db.Add(new DbItem("mediaItem", mediaItemId)
      {
        {
          "Mime type", "video/fake"
        }
      });
      var homeItem = db.GetItem(homeItemId);

      var htmlString = homeItem.RenderParallaxMediaAttributes();
      var attributes = ToAttributesDictionary(htmlString);

      attributes["data-multibackground-layer-0-attachment"].Should().Be("parallax");
      attributes["data-multibackground-layer-0-parallaxspeed"].Should().Be(parallaxSpeed);
      attributes["data-multibackground"].Should().BeEmpty();
      attributes["data-multibackground-layer-0-type"].Should().Be("video");
      attributes["data-multibackground-layer-0-format"].Should().Be("fake");
    }

    private static Dictionary<string, string> ToAttributesDictionary(HtmlString htmlString)
    {
      return htmlString.ToString().Split(' ').Select(x => x.Split('=')).ToDictionary(x => x[0], val => val.Length == 1 ? "" : val[1].Trim('\'', '"'));
    }
  }
}