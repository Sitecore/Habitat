namespace Sitecore.Feature.Media.Tests.Infrastructure
{
  using Sitecore.FakeDb;

  public class MediaTemplate : DbTemplate
  {
    public MediaTemplate() : base(Templates.HasMedia.ID)
    {
      base.Add(Templates.HasMedia.Fields.MediaThumbnail);
      base.BaseIDs = new[]
      {
        Templates.HasMediaVideo.ID
      };
    }
  }
}