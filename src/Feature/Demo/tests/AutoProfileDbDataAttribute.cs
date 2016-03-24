namespace Sitecore.Feature.Demo.Tests
{
  using Ploeh.AutoFixture;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.Foundation.Testing.Attributes;

  public class AutoProfileDbDataAttribute : AutoDbDataAttribute
  {
    public AutoProfileDbDataAttribute()
    {
      var db = Fixture.Create<Db>();
      db.Add(new DbTemplate(Templates.ProfilingSettings.ID)
      {
        new DbField(Templates.ProfilingSettings.Fields.SiteProfiles)
      });

      db.Add(new DbTemplate(ProfileItem.TemplateID)
      {
        new DbField(ProfileItem.FieldIDs.Type),
        new DbField(ProfileItem.FieldIDs.NameField),
      });


      db.Add(new DbTemplate(PatternCardItem.TemplateID)
      {
        new DbField( PatternCardItem.FieldIDs.Pattern)
      });




    }
  }
}