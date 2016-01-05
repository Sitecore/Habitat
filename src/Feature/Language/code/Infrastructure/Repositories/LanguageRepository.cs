namespace Sitecore.Feature.Language.Infrastructure.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore;
  using Sitecore.Data.Fields;
  using Sitecore.Feature.Language.Infrastructure.Factories;
  using Sitecore.Feature.Language.Models;
  using Sitecore.Foundation.MultiSite;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public static class LanguageRepository
  {
    public static IEnumerable<Language> GetAll()
    {
      var languages = Context.Database.GetLanguages();
      return languages.Select(LanguageFactory.Create);
    }

    public static Language GetActive()
    {
      return LanguageFactory.Create(Context.Language);
    }

    public static IEnumerable<Language> GetSupportedLanguages()
    {
      var languages = GetAll();
      var siteContext = new SiteContext();
      var siteDefinition = siteContext.GetSiteDefinition(Sitecore.Context.Item);
      
      if (siteDefinition?.Item == null)
      {
        return languages;
      }

      if (siteDefinition.Item.IsDerived(Feature.Language.Templates.LanguageSettings.ID))
      {
        var supportedLanguagesField = new MultilistField(siteDefinition.Item.Fields[Feature.Language.Templates.LanguageSettings.Fields.SupportedLanguages]);
        if (supportedLanguagesField.Count == 0)
        {
          return languages;
        }

        var supportedLanguages = supportedLanguagesField.GetItems();

        languages = languages.Where(language => supportedLanguages.Any(item => item.Name.Equals(language.TwoLetterCode)));
      }

      return languages;
    } 
  }
}