using System.Collections.Generic;

namespace Sitecore.Feature.Language.Models
{
    public class LanguageSelector
    {
        public Language ActiveLanguage { get; set; }
        public IList<Language> SupportedLanguages { get; set; }
    }
}