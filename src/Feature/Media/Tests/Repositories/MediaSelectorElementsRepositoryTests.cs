namespace Sitecore.Feature.Media.Tests.Repositories
{
  using System.Linq;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Media.Infrastructure.Repositories;
  using Sitecore.Feature.Media.Tests.Infrastructure;
  using Xunit;

  public class MediaSelectorElementsRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetShouldReturnEmptyForEmptyItems([Content] Item item)
    {
      // substitute the original provider with the mocked one
      MediaSelectorElementsRepository.Get(item).Count().Should().Be(0);
    }

    [Theory]
    [AutoDbData]
    public void GetShouldReturnEmptyForEmptyVideoItems([Content] Item item)
    {
      var child = item.Add("childVideo", new TemplateID(Templates.HasMediaVideo.ID));

      using (new EditContext(item))
      {
        item[Templates.HasMediaSelector.Fields.MediaSelector] = child.ID.ToString();
      }
      // substitute the original provider with the mocked one
      MediaSelectorElementsRepository.Get(item).Count().Should().Be(0);
    }


    [Theory]
    [AutoDbData]
    public void GetShouldReturnCollectionForValidVideoLinksItems([Content] Item item, [Content] MediaTemplate mediaTemplate, [Content] MediaSelectorTemplate selectorTemplate, [Content] VideoTemplate vt)
    {
      var child = item.Add("childVideo", new TemplateID(Templates.HasMedia.ID));

      using (new EditContext(child))
      {
        child[Templates.HasMediaVideo.Fields.VideoLink] = "videoLink";
      }


      var selector = item.Add("selector", new TemplateID(Templates.HasMediaSelector.ID));
      using (new EditContext(selector))
      {
        selector[Templates.HasMediaSelector.Fields.MediaSelector] = child.ID.ToString();
      }
      // substitute the original provider with the mocked one
      var carouselElements = MediaSelectorElementsRepository.Get(selector);
      carouselElements.Count().Should().Be(1);
      carouselElements.First().Item.ID.Should().Be(child.ID);
      carouselElements.First().Item[Templates.HasMediaVideo.Fields.VideoLink].Should().Be("videoLink");
    }

    [Theory]
    [AutoDbData]
    public void GetShouldReturnCollectionForVideoLinkWithThumbnail([Content] Item item, [Content] MediaTemplate mediaTemplate, [Content] MediaSelectorTemplate selectorTemplate, [Content] VideoTemplate vt)
    {
      var child = item.Add("childVideo", new TemplateID(Templates.HasMedia.ID));

      using (new EditContext(child))
      {
        child[Templates.HasMedia.Fields.Thumbnail] = "videoLink";
      }


      var selector = item.Add("selector", new TemplateID(Templates.HasMediaSelector.ID));
      using (new EditContext(selector))
      {
        selector[Templates.HasMediaSelector.Fields.MediaSelector] = child.ID.ToString();
      }
      // substitute the original provider with the mocked one
      var carouselElements = MediaSelectorElementsRepository.Get(selector);
      carouselElements.Count().Should().Be(1);
      carouselElements.First().Item.ID.Should().Be(child.ID);
      carouselElements.First().Item[Templates.HasMedia.Fields.Thumbnail].Should().Be("videoLink");
    }


    [Theory]
    [AutoDbData]
    public void GetShouldIgnoreEmptyVideoLinksItems([Content] Item item, [Content] MediaTemplate mediaTemplate, [Content] MediaSelectorTemplate selectorTemplate, [Content] VideoTemplate vt)
    {
      var child = item.Add("childVideo", new TemplateID(Templates.HasMedia.ID));

      var selector = item.Add("selector", new TemplateID(Templates.HasMediaSelector.ID));
      using (new EditContext(selector))
      {
        selector[Templates.HasMediaSelector.Fields.MediaSelector] = child.ID.ToString();
      }
      // substitute the original provider with the mocked one
      var carouselElements = MediaSelectorElementsRepository.Get(selector);
      carouselElements.Count().Should().Be(0);
    }
  }
}