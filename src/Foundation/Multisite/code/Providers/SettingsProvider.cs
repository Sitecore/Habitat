namespace Sitecore.Foundation.Multisite.Providers
{
  using System.Linq;
  using Sitecore.Configuration;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Web;

  public class SettingsProvider : ISettingsProvider
  {
    private readonly SiteContext siteContext;

    public SettingsProvider() : this(new SiteContext())
    { 
    }

    public SettingsProvider(SiteContext siteContext)
    {
      this.siteContext = siteContext;
    }

    public static string SettingsRootName => Settings.GetSetting("Multisite.SettingsRootName", "settings");

    public virtual Item GetSettingItem(string settingName, Item contextItem)
    {

      var currentDefinition = this.siteContext.GetSiteDefinition(contextItem);
      if (currentDefinition == null)
      {
        return null;
      }

      var definitionItem = currentDefinition.Item;
      var settingsFolder = definitionItem.Children[SettingsRootName];

      var sourceSettingItem = settingsFolder?.Children.FirstOrDefault(x => x.IsDerived(Templates.SiteSettings.ID) && x.Key.Equals(settingName.ToLower()));

      return sourceSettingItem;
    }

    public virtual SiteInfo GetCurrentSiteInfo(Item contextItem)
    {
      var context = new SiteContext();

      var currentDefinition = context.GetSiteDefinition(contextItem);
      if (currentDefinition == null)
      {
        {
          return null;
        }
      }

      var siteInfo = Factory.GetSiteInfo(currentDefinition.Name);
      return siteInfo;
    }
  }
}