namespace Sitecore.Feature.Media.Tests.Infrastructure
{
  using Sitecore.FakeDb;

  public class MediaSelectorTemplate : DbTemplate
  {
    public MediaSelectorTemplate() : base(Templates.HasMediaSelector.ID)
    {
      base.Add(Templates.HasMediaSelector.Fields.MediaSelector);
    }
  }
}