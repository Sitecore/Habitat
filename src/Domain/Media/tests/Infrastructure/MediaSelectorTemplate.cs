namespace Habitat.Media.Tests.Repositories
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