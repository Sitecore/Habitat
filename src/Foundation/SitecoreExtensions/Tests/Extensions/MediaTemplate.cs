namespace Sitecore.Foundation.SitecoreExtensions.Tests.Extensions
{
  using Sitecore.Data;
  using Sitecore.FakeDb;

  public class MediaTemplate : DbTemplate
  {
    public MediaTemplate()
    {
      Add(new DbField("medialink", FieldId));
    }

    public ID FieldId { get; } = ID.NewID;
  }
}