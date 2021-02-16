namespace Sitecore.Foundation.Multisite.Providers
{
    using System.Linq;
    using Sitecore.Configuration;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.DependencyInjection;

    [Service(typeof(ISiteSettingsProvider))]
    public class SiteSettingsProvider : ISiteSettingsProvider
    {
        private readonly SiteContext siteContext;

        public SiteSettingsProvider(SiteContext siteContext)
        {
            this.siteContext = siteContext;
        }

        public static string SettingsRootName => Settings.GetSetting("Multisite.SettingsRootName", "settings");

        public virtual Item GetSetting(Item contextItem, string settingsName, string setting)
        {
            var settingsRootItem = this.GetSettingsRoot(contextItem, settingsName);
            var settingItem = settingsRootItem?.Children.FirstOrDefault(i => i.Key.Equals(setting.ToLower()));
            return settingItem;
        }

        private Item GetSettingsRoot(Item contextItem, string settingsName)
        {
            var currentDefinition = this.siteContext.GetSiteDefinition(contextItem);
            if (currentDefinition?.Item == null)
            {
                return null;
            }

            var definitionItem = currentDefinition.Item;
            var settingsFolder = definitionItem.Children[SettingsRootName];
            var settingsRootItem = settingsFolder?.Children.FirstOrDefault(i => i.DescendsFrom(Templates.SiteSettings.ID) && i.Key.Equals(settingsName.ToLower()));
            return settingsRootItem;
        }
    }
}