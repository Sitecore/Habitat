using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Language.Models
{
    public class LanguageSelector
    {
        public Language ActiveLanguage { get; set; }
        public IList<Language> SupportedLanguages { get; set; }
    }
}