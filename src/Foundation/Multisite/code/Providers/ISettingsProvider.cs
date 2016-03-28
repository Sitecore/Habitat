namespace Sitecore.Foundation.Multisite.Providers
{
  using Sitecore.Data.Items;
  using Sitecore.Web;

  public interface ISettingsProvider
  {
    Item GetSettingItem(string name, Item contextItem);

    SiteInfo GetCurrentSiteInfo(Item contextItem);
  }
}