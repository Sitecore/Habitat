namespace Sitecore.Feature.Multisite.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Feature.Multisite.Models;
  using Sitecore.Foundation.Multisite;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class SiteConfigurationRepository : ISiteConfigurationRepository
  {
    private readonly ISiteDefinitionsProvider siteDefinitionsProvider;

    public SiteConfigurationRepository() : this(new SiteDefinitionsProvider())
    {
    }

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
      return item.IsDerived(Multisite.Templates.SiteConfiguration.ID);
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
      var title = siteConfiguration.Item.GetString(Multisite.Templates.SiteConfiguration.Fields.Title);
      if (string.IsNullOrEmpty(title))
      {
        title = siteConfiguration.Name;
      }
      return new SiteConfiguration
             {
               HostName = siteConfiguration.HostName,
               Name = siteConfiguration.Name,
               Title = title,
               ShowInMenu = siteConfiguration.Item.GetCheckBoxValue(Multisite.Templates.SiteConfiguration.Fields.ShowInMenu),
               IsCurrent = siteConfiguration.IsCurrent
             };
    }
  }
}