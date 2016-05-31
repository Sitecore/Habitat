namespace Sitecore.Feature.Language.Tests.Extensions
{
  using Ploeh.AutoFixture;
  using Sitecore.FakeDb;
  using Sitecore.Foundation.Multisite;

  public class AutoLanguageDbDataAttribute : AutoDbDataAttribute
  {
    public AutoLanguageDbDataAttribute()
    {
      var db = this.Fixture.Create<Db>();
      db.Add(new DbTemplate(Templates.Site.ID));
      db.Add(new DbTemplate(Language.Templates.LanguageSettings.ID)
      {
        Fields =
        {
          {
            Language.Templates.LanguageSettings.Fields.SupportedLanguages, string.Empty
          }
        }
      });
    }
  }
}