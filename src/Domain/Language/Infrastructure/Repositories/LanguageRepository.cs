using System.Collections.Generic;
using System.Linq;
using Habitat.Language.Infrastructure.Factories;
using Sitecore;

namespace Habitat.Language.Infrastructure.Repositories
{
    public static class LanguageRepository
    {
        public static IEnumerable<Models.Language> GetCountries()
        {
            var languages = Context.Database.GetLanguages();
            return languages.Select(LanguageFactory.Create);
        }
    }
}