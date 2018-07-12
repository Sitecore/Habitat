using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.Language.Repositories
{
    using Sitecore.Feature.Language.Models;

    public interface ILanguageRepository
    {
        Language GetActive();
        IEnumerable<Language> GetSupportedLanguages();
    }
}
