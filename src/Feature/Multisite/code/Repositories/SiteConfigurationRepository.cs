namespace Sitecore.Feature.Multisite.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Configuration;
    using Sitecore.Data.Items;
    using Sitecore.Feature.Multisite.Models;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Multisite;
    using Sitecore.Foundation.Multisite.Providers;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Globalization;

    [Service(typeof(ISiteConfigurationRepository))]
    public class SiteConfigurationRepository : ISiteConfigurationRepository
    {
        private readonly ISiteDefinitionsProvider siteDefinitionsProvider;

        public SiteConfigurationRepository(ISiteDefinitionsProvider itemSiteDefinitionsProvider)
        {
            this.siteDefinitionsProvider = itemSiteDefinitionsProvider;
        }

        public SiteConfigurations Get()
        {
            var siteDefinitions = this.siteDefinitionsProvider.SiteDefinitions;
            return this.Create(siteDefinitions);
        }

        private bool IsValidSiteConfiguration(SiteDefinition siteDefinition)
        {
            return siteDefinition.Item != null && this.IsSiteConfigurationItem(siteDefinition.Item);
        }

        private bool IsSiteConfigurationItem(Item item)
        {
            return item.DescendsFrom(Multisite.Templates.SiteConfiguration.ID);
        }

        private SiteConfigurations Create(IEnumerable<SiteDefinition> definitions)
        {
            var siteDefinitions = new SiteConfigurations
            {
                Items = definitions.Where(this.IsValidSiteConfiguration).Select(CreateSiteConfiguration).Where(sc => sc.ShowInMenu)
            };
            return siteDefinitions;
        }

        private static SiteConfiguration CreateSiteConfiguration(SiteDefinition siteConfiguration)
        {
            return new SiteConfiguration
            {
                HostName = siteConfiguration.HostName,
                Name = siteConfiguration.Name,
                Title = GetSiteTitle(siteConfiguration),
                ShowInMenu = siteConfiguration.Item.Fields[Multisite.Templates.SiteConfiguration.Fields.ShowInMenu].IsChecked(),
                IsCurrent = siteConfiguration.IsCurrent
            };
        }

        private static Item GetSiteItemInSiteLanguage(SiteDefinition siteConfiguration)
        {
            var siteItem = siteConfiguration.Item;
            var siteLanguage = string.IsNullOrEmpty(siteConfiguration.Site.Language) ? Settings.DefaultLanguage : siteConfiguration.Site.Language;
            using (new LanguageSwitcher(siteLanguage))
            {
                var item = siteConfiguration.Item.Database.GetItem(siteConfiguration.Item.ID);
                if (item.HasContextLanguage())
                    siteItem = item;
            }
            return siteItem;
        }

        private static string GetSiteTitle(SiteDefinition siteConfiguration)
        {
            var siteItem = GetSiteItemInSiteLanguage(siteConfiguration);

            var title = (siteItem ?? siteConfiguration.Item)[Multisite.Templates.SiteConfiguration.Fields.Title];
            if (string.IsNullOrEmpty(title))
                title = siteConfiguration.Name;
            return title;
        }
    }
}