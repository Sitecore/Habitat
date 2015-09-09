using System.Collections.Generic;
using System.Linq;
using Habitat.Language.Infrastructure.Factories;
using Sitecore;

namespace Habitat.Language.Infrastructure.Repositories
{
    public static class LanguageRepository
    {
        public static IEnumerable<Models.Language> GetAll()
        {
            var languages = Context.Database.GetLanguages();
            return languages.Select(LanguageFactory.Create);
        }
        public static Models.Language GetActive()
        {
            return LanguageFactory.Create(Context.Language);
        }
    }
}