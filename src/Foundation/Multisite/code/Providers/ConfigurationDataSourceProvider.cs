namespace Sitecore.Foundation.MultiSite.Providers
{
  using Sitecore.Data;
  using Sitecore.Data.Items;

  public class ConfigurationDatasourceProvider : IDatasourceProvider
  {
    private const string datasourceLocationPostfix = "datasourceLocation";
    private const string datasourceTemplatePostfix = "datasourceTemplate";
    private readonly ISettingsProvider settingsProvider;

    public ConfigurationDatasourceProvider() : this(new SettingsProvider())
    {
    }

    public ConfigurationDatasourceProvider(ISettingsProvider settingsProvider)
    {
      this.settingsProvider = settingsProvider;
    }

    public virtual Item[] GetDatasources(string name, Item contextItem)
    {
      var siteInfo = settingsProvider.GetCurrentSiteInfo(contextItem);
      if (siteInfo == null)
      {
        return new Item[] { };
      }

      var datasourceLocationPropertyName = $"{name}.{datasourceLocationPostfix}";
      var datasourceLocation = siteInfo.Properties[datasourceLocationPropertyName];

      if (string.IsNullOrEmpty(datasourceLocation))
      {
        return new Item[] { };
      }

      var item = Database.GetItem(datasourceLocation);
      return item == null ? (new Item[] { }) : new[] { item };
    }

    public virtual Item GetDatasourceTemplate(string settingName, Item contextItem)
    {
      var siteInfo = settingsProvider.GetCurrentSiteInfo(contextItem);
      if (siteInfo == null)
      {
        return null;
      }

      var datasourceTemplatePropertyName = $"{settingName}.{datasourceTemplatePostfix}";
      var datasourceTemplate = siteInfo.Properties[datasourceTemplatePropertyName];

      return string.IsNullOrEmpty(datasourceTemplate) ? null : Database.GetItem(datasourceTemplate);
    }

    public Database Database { get; set; }
  }
}