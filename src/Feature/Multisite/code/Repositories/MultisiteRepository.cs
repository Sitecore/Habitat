namespace Sitecore.Feature.Multisite.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using Sitecore.Data.Items;
  using Sitecore.Feature.Multisite.Models;
  using Sitecore.Foundation.Multisite;
  using Sitecore.Foundation.Multisite.Providers;
  using Sitecore.Foundation.SitecoreExtensions;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class MultisiteRepository : IMultisiteRepository
  {
    private ISiteDefinitionsProvider itemSiteDefinitionsProvider;

    public MultisiteRepository() : this(new ItemSiteDefinitionsProvider())
    {
    }

    public MultisiteRepository(ISiteDefinitionsProvider itemSiteDefinitionsProvider)
    {
      this.itemSiteDefinitionsProvider = itemSiteDefinitionsProvider;
    }

    public SiteDefinitions GetSiteDefinitions()
    {
      var provider = new ItemSiteDefinitionsProvider();
      var siteDefinitions = provider.SiteDefinitions;
      var siteConfigurationItems = siteDefinitions.Where(siteDefinition => siteDefinition.Item != null && this.IsSiteConfigurationItem(siteDefinition.Item));
      return this.Create(siteConfigurationItems);
    }

    private bool IsSiteConfigurationItem(Item item)
    {
      return item.IsDerived(Multisite.Templates.SiteConfiguration.ID);
    }

    private SiteDefinitions Create(IEnumerable<SiteDefinitionItem> definitions)
    {
      var siteDefinitions = new SiteDefinitions();
      siteDefinitions.Sites = definitions.Where(siteConfigurationItem => siteConfigurationItem.Item[Multisite.Templates.SiteConfiguration.Fields.ShowInMenu] == "1").Select(siteConfiguration => new SiteDefinition {HostName = siteConfiguration.HostName, Name = siteConfiguration.Name, IsCurrent = siteConfiguration.IsCurrent});
      return siteDefinitions;
    }
  }
}