namespace Sitecore.Foundation.Multisite.Providers
{
    using Sitecore.Data.Items;

    public interface ISiteSettingsProvider
  {
    Item GetSetting(Item contextItem, string settingsType, string setting);
  }
}