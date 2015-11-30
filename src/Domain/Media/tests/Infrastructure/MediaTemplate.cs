namespace Habitat.Media.Tests.Repositories
{
  using Sitecore.FakeDb;

  public class MediaTemplate:DbTemplate
  {
    public MediaTemplate():base(Templates.HasMedia.ID)
    {
      base.Add(Templates.HasMedia.Fields.Thumbnail);
      base.BaseIDs = new[]
      {
        Templates.HasMediaVideo.ID
      };
    }
  }
}