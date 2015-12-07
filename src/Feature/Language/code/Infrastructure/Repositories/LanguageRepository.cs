namespace Sitecore.Feature.Language.Infrastructure.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore;
  using Sitecore.Feature.Language.Infrastructure.Factories;
  using Sitecore.Feature.Language.Models;

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
  }
}