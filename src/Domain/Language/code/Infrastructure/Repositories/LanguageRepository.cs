namespace Habitat.Language.Infrastructure.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Habitat.Language.Infrastructure.Factories;
  using Habitat.Language.Models;
  using Sitecore;

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