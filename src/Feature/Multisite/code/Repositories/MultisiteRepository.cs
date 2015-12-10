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
    public SiteDefinitions GetSiteDefinitions()
    {
      var provider = new ItemSiteDefinitionsProvider();
      var siteDefinitions = provider.SiteDefinitions;
      var siteConfigurationItems = siteDefinitions.Where(siteDefinition => siteDefinition.Item != null && siteDefinition.Item.IsDerived(Multisite.Templates.SiteConfiguration.ID));
      return this.Create(siteConfigurationItems);
    }

    private bool IsSiteConfigurationItem(Item item)
    {
      return item.IsDerived(Multisite.Templates.SiteConfiguration.ID);
    }

    private SiteDefinitions Create(IEnumerable<SiteDefinitionItem> definitions)
    {
      var siteDefinitions = new SiteDefinitions();
      siteDefinitions.Sites = definitions.Where(siteConfigurationItem => siteConfigurationItem.Item[Multisite.Templates.SiteConfiguration.Fields.ShowInMenu] == "1").Select(siteConfiguration => new SiteDefinition {HostName = siteConfiguration.HostName, Name = siteConfiguration.Name});
      return siteDefinitions;
    }
  }
}