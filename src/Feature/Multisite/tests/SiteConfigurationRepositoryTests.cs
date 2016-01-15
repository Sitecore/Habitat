namespace Sitecore.Feature.MultiSite.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Multisite.Tests.Extensions;
  using Sitecore.Feature.MultiSite.Models;
  using Sitecore.Feature.MultiSite.Repositories;
  using Sitecore.Foundation.MultiSite;
  using Sitecore.Foundation.MultiSite.Providers;
  using Sitecore.Publishing.Explanations;
  using Sitecore.SecurityModel;
  using Xunit;

  public class SiteConfigurationRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Constructor_InstanceOfISiteDefinitionInterface_InstanceShouldBeCreated(ISiteDefinitionsProvider provider, SiteConfigurationRepository multisiteRepository)
    {
      Action action = () => new SiteConfigurationRepository(provider);
      action.ShouldNotThrow();
    }

    [Theory]
    [AutoDbData]
    public void GetSiteDefinitions_ShouldReturnSiteDefinitiosModel([Frozen]ISiteDefinitionsProvider siteDefinitionProvider, [Greedy] SiteConfigurationRepository repository, string name)
    {
      var id = ID.NewID;
      var db = new Db
      {
        new DbItem(name, id, MultiSite.Templates.SiteConfiguration.ID)
        {
          new DbField(MultiSite.Templates.SiteConfiguration.Fields.ShowInMenu)
          {
            {"en", "1"}
          }
        }
      };

      var item = db.GetItem(id);

      siteDefinitionProvider.SiteDefinitions.Returns(new List<Foundation.MultiSite.SiteDefinition> { new Foundation.MultiSite.SiteDefinition { Item = item } });
      var definitions = repository.Get();
      definitions.Should().BeOfType<SiteConfigurations>();
      var sites = definitions.Items.ToList();
      sites.Count.Should().BeGreaterThan(0);
    }

    public class SiteTemplate : DbTemplate
    {
      public SiteTemplate()
      {
        this.Add(Templates.Site.Fields.HostName);
        this.Add(MultiSite.Templates.SiteConfiguration.Fields.ShowInMenu);
      }
    }
  }
}
