namespace Sitecore.Foundation.Multisite.Providers
{
  using Sitecore.Data.Items;
  using Sitecore.Web;

  public interface ISiteSettingsProvider
  {
    Item GetSetting(Item contextItem, string settingsType, string setting);
  }
}