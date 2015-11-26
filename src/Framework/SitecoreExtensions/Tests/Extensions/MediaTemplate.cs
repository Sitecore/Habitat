namespace Habitat.Framework.SitecoreExtensions.Tests.Extensions
{
  using Sitecore.Data;
  using Sitecore.FakeDb;

  public class MediaTemplate : DbTemplate
  {
    public MediaTemplate()
    {
      this.Add(new DbField("medialink",this.FieldId));
    }

    public ID FieldId { get
    {
      return this.field;
    } }

    private readonly ID field = ID.NewID;
  }
}