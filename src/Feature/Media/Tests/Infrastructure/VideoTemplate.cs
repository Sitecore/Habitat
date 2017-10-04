namespace Sitecore.Feature.Media.Tests.Infrastructure
{
  using Sitecore.FakeDb;

  public class VideoTemplate : DbTemplate
  {
    public VideoTemplate() : base(Templates.HasMediaVideo.ID)
    {
      base.Add(Templates.HasMediaVideo.Fields.MediaVideoLink);

    }
  }
}