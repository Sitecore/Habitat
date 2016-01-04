namespace Sitecore.Feature.MultiSite.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Feature.MultiSite.Models;
  using Sitecore.Foundation.MultiSite;
  using Sitecore.Foundation.MultiSite.Providers;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class SiteDefinitionsRepository : ISiteDefinitionsRepository
  {
    private readonly ISiteDefinitionsProvider siteDefinitionsProvider;

    public SiteDefinitionsRepository() : this(new ItemSiteDefinitionsProvider())
    {
    }

    public SiteDefinitionsRepository(ISiteDefinitionsProvider itemSiteDefinitionsProvider)
    {
      siteDefinitionsProvider = itemSiteDefinitionsProvider;
    }

    public SiteDefinitions Get()
    {
      var siteDefinitions = siteDefinitionsProvider.SiteDefinitions;
      var siteConfigurationItems = siteDefinitions.Where(IsValidSiteDefinition);
      return Create(siteConfigurationItems);
    }

    private bool IsValidSiteDefinition(Foundation.MultiSite.SiteDefinition siteDefinition)
    {
      return siteDefinition.Item != null && IsSiteConfigurationItem(siteDefinition.Item);
    }

    public void ConfigureHostName(string name, string hostName, bool showInMenu = true)
    {
      var siteDefinition = siteDefinitionsProvider.SiteDefinitions.Single(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
      using (new EditContext(siteDefinition.Item))
      {
        siteDefinition.Item[Templates.Site.Fields.HostName] = hostName;
        ((CheckboxField)siteDefinition.Item.Fields[MultiSite.Templates.SiteConfiguration.Fields.ShowInMenu]).Checked = showInMenu;
      }
    }

    private bool IsSiteConfigurationItem(Item item)
    {
      return item.IsDerived(MultiSite.Templates.SiteConfiguration.ID);
    }

    private SiteDefinitions Create(IEnumerable<Foundation.MultiSite.SiteDefinition> definitions)
    {
      var siteDefinitions = new SiteDefinitions
                            {
                              Sites = definitions.Where(IsValidSiteDefinition).Select(CreateSiteDefinition)
                            };
      return siteDefinitions;
    }

    private static Models.SiteDefinition CreateSiteDefinition(Foundation.MultiSite.SiteDefinition siteConfiguration)
    {
      return new Models.SiteDefinition
             {
               HostName = siteConfiguration.HostName,
               Name = siteConfiguration.Name,
               IsCurrent = siteConfiguration.IsCurrent
             };
    }

    private static Func<Foundation.MultiSite.SiteDefinition, bool> IsValidSiteDefinition()
    {
      return siteConfigurationItem => siteConfigurationItem.Item[MultiSite.Templates.SiteConfiguration.Fields.ShowInMenu] == "1";
    }
  }
}