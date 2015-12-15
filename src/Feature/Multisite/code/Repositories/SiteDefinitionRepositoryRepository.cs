namespace Sitecore.Feature.MultiSite.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using Sitecore.Data.Items;
  using Sitecore.Feature.MultiSite.Models;
  using Sitecore.Foundation.MultiSite;
  using Sitecore.Foundation.MultiSite.Providers;
  using Sitecore.Foundation.SitecoreExtensions;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class SiteDefinitionRepositoryRepository : ISiteDefinitionRepositoryRepository
  {
    private ISiteDefinitionsProvider siteDefinitionsProvider;

    public SiteDefinitionRepositoryRepository() : this(new ItemSiteDefinitionsProvider())
    {
    }

    public SiteDefinitionRepositoryRepository(ISiteDefinitionsProvider itemSiteDefinitionsProvider)
    {
      this.siteDefinitionsProvider = itemSiteDefinitionsProvider;
    }

    public SiteDefinitions Get()
    {
      var siteDefinitions = this.siteDefinitionsProvider.SiteDefinitions;
      var siteConfigurationItems = siteDefinitions.Where(siteDefinition => siteDefinition.Item != null && this.IsSiteConfigurationItem(siteDefinition.Item));
      return this.Create(siteConfigurationItems);
    }

    private bool IsSiteConfigurationItem(Item item)
    {
      return item.IsDerived(MultiSite.Templates.SiteConfiguration.ID);
    }

    private SiteDefinitions Create(IEnumerable<SiteDefinitionItem> definitions)
    {
      var siteDefinitions = new SiteDefinitions();
      siteDefinitions.Sites = definitions.Where(siteConfigurationItem => siteConfigurationItem.Item[MultiSite.Templates.SiteConfiguration.Fields.ShowInMenu] == "1").Select(siteConfiguration => new SiteDefinition {HostName = siteConfiguration.HostName, Name = siteConfiguration.Name, IsCurrent = siteConfiguration.IsCurrent});
      return siteDefinitions;
    }
  }
}