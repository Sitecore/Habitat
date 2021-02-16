using System.Collections.Generic;

namespace Sitecore.Feature.Language.Repositories
{
    using Sitecore.Feature.Language.Models;

    public interface ILanguageRepository
    {
        Language GetActive();
        IEnumerable<Language> GetSupportedLanguages();
    }
}
