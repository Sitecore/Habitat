namespace Sitecore.Feature.MultiSite.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Feature.MultiSite.Models;
  using Sitecore.Foundation.MultiSite;
  using Sitecore.Foundation.MultiSite.Providers;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class SiteConfigurationRepository : ISiteConfigurationRepository
  {
    private readonly ISiteDefinitionsProvider siteDefinitionsProvider;

    public SiteConfigurationRepository() : this(new ItemSiteDefinitionsProvider())
    {
    }

    public SiteConfigurationRepository(ISiteDefinitionsProvider itemSiteDefinitionsProvider)
    {
      siteDefinitionsProvider = itemSiteDefinitionsProvider;
    }

    public SiteConfigurations Get()
    {
      var siteDefinitions = siteDefinitionsProvider.SiteDefinitions;
      return Create(siteDefinitions);
    }

    private bool IsValidSiteConfiguration(SiteDefinition siteDefinition)
    {
      return siteDefinition.Item != null && IsSiteConfigurationItem(siteDefinition.Item);
    }

    private bool IsSiteConfigurationItem(Item item)
    {
      return item.IsDerived(MultiSite.Templates.SiteConfiguration.ID);
    }

    private SiteConfigurations Create(IEnumerable<SiteDefinition> definitions)
    {
      var siteDefinitions = new SiteConfigurations
                            {
                              Items = definitions.Where(IsValidSiteConfiguration).Select(CreateSiteConfiguration).Where(sc => sc.ShowInMenu)
                            };
      return siteDefinitions;
    }

    private static SiteConfiguration CreateSiteConfiguration(SiteDefinition siteConfiguration)
    {
      var title = siteConfiguration.Item.GetString(MultiSite.Templates.SiteConfiguration.Fields.Title);
      if (string.IsNullOrEmpty(title))
      {
        title = siteConfiguration.Name;
      }
      return new SiteConfiguration
             {
               HostName = siteConfiguration.HostName,
               Name = siteConfiguration.Name,
               Title = title,
               ShowInMenu = siteConfiguration.Item.GetCheckBoxValue(MultiSite.Templates.SiteConfiguration.Fields.ShowInMenu),
               IsCurrent = siteConfiguration.IsCurrent
             };
    }
  }
}