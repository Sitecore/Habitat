namespace Habitat.Media.Tests.Repositories
{
  using Sitecore.FakeDb;

  public class VideoTemplate : DbTemplate
  {
    public VideoTemplate() : base(Templates.HasMediaVideo.ID)
    {
      base.Add(Templates.HasMediaVideo.Fields.VideoLink);

    }
  }
}