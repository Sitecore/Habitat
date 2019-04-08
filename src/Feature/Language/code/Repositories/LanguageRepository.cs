namespace Sitecore.Feature.Language.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Abstractions;
    using Sitecore.Data.Fields;
    using Sitecore.DependencyInjection;
    using Sitecore.Feature.Language.Models;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Multisite;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Globalization;
    using Sitecore.Links;

    [Service(typeof(ILanguageRepository))]
    public class LanguageRepository : ILanguageRepository
    {
        private SiteContext SiteContext { get; }
        private BaseLinkManager LinkManager { get; }

        public LanguageRepository(SiteContext siteContext, BaseLinkManager linkManager)
        {
            this.SiteContext = siteContext;
            this.LinkManager = linkManager;
        }

        private IEnumerable<Models.Language> GetAll()
        {
            var languages = Context.Database.GetLanguages();
            return languages.Select(this.Create);
        }

        public Models.Language GetActive()
        {
            return this.Create(Context.Language);
        }

        public IEnumerable<Models.Language> GetSupportedLanguages()
        {
            var languages = this.GetAll();
            var siteDefinition = this.SiteContext.GetSiteDefinition(Context.Item);

            if (siteDefinition?.Item == null || !siteDefinition.Item.DescendsFrom(Feature.Language.Templates.LanguageSettings.ID))
            {
                return languages;
            }

            var supportedLanguagesField = new MultilistField(siteDefinition.Item.Fields[Feature.Language.Templates.LanguageSettings.Fields.SupportedLanguages]);
            if (supportedLanguagesField.Count == 0)
            {
                return Enumerable.Empty<Models.Language>();
            }

            var supportedLanguages = supportedLanguagesField.GetItems();

            languages = languages.Where(language => supportedLanguages.Any(item => item.Name.Equals(language.Name)));

            return languages;
        }

        private Models.Language Create(Globalization.Language language)
        {
            return new Models.Language
            {
                Name = language.Name,
                NativeName = language.CultureInfo.NativeName,
                Url = Context.Item != null ? GetItemUrlByLanguage(language) : null,
                Icon = string.Concat("/~/icon/", language.GetIcon(Context.Database)),
                TwoLetterCode = language.CultureInfo.TwoLetterISOLanguageName
            };
        }

        private string GetItemUrlByLanguage(Globalization.Language language)
        {
            using (new LanguageSwitcher(language))
            {
                var options = new UrlOptions
                {
                    AlwaysIncludeServerUrl = true,
                    LanguageEmbedding = LanguageEmbedding.Always,
                    LowercaseUrls = true
                };
                var url = this.LinkManager.GetItemUrl(Context.Item, options);
                return StringUtil.EnsurePostfix('/', url).ToLower();
            }
        }
    }
}